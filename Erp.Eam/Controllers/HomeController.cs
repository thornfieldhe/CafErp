// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManageController.cs" company="">
//   
// </copyright>
// <summary>
//   The manage controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Erp.Eam.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using CAF;

    using Erp.Eam.Business;
    using Erp.Eam.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;

    /// <summary>
    /// The manage controller.
    /// </summary>
    [Authorize]
    public class HomeController : Controller
    {


        /// <summary>
        /// Gets the sign in manager.
        /// </summary>
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return this.signInManager ?? this.HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }

            private set
            {
                this.signInManager = value;
            }
        }

        /// <summary>
        /// Gets the user manager.
        /// </summary>
        public ApplicationUserManager UserManager
        {
            get
            {
                return this.userManager ?? this.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }

            private set
            {
                this.userManager = value;
            }
        }

        /// <summary>
        /// Gets the user manager.
        /// </summary>
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return this.roleManager ?? ApplicationRoleManager.CreateForEF();
            }

            private set
            {
                this.roleManager = value;
            }
        }

        /// <summary>
        /// 授权管理器
        /// </summary>
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return this.HttpContext.GetOwinContext().Authentication;
            }
        }

        /// <summary>
        /// The _sign in manager.
        /// </summary>
        private ApplicationSignInManager signInManager;

        /// <summary>
        /// The _user manager.
        /// </summary>
        private ApplicationUserManager userManager;

        /// <summary>
        /// The _role manager.
        /// </summary>
        private ApplicationRoleManager roleManager;

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            var user = this.UserManager.Users.Single(r => r.UserName == this.User.Identity.Name);
            return this.View(user);
        }


        [AllowAnonymous]
        public ActionResult Login()
        {
            return this.View();
        }

        public ActionResult Dashboard()
        {
            return this.PartialView("_Dashboard");
        }

        /// <summary>
        /// The login.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login(LoginUser user)
        {
            try
            {
                var result =
                    await this.SignInManager.PasswordSignInAsync(user.Name, user.Password, true, shouldLockout: false);

                switch (result)
                {
                    case SignInStatus.Success:
                        return this.Json(new ActionResultData<string>("/Home/Index"), JsonRequestBehavior.AllowGet);
                    default:
                        return this.Json(new ActionResultStatus(10, "用户名或密码错误"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return this.Json(new ActionResultStatus(ex), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SignOut()
        {
            this.AuthenticationManager.SignOut();
            return this.View("Login");
        }

        public ActionResult ChangePasswordIndex()
        {
            return this.PartialView("_ChangePassword");
        }

        public ActionResult UserIndex()
        {
            var roles = this.RoleManager.Roles.ToList();
            return this.PartialView("_UserIndex", roles);
        }

        [Authorize(Roles = "Admins")]
        public ActionResult GetUserList(int pageIndex, int pageSize = 20)
        {
            var roles = this.RoleManager.Roles.ToList();
            var users = this.UserManager.Users.ToList();
            var list = new List<UserInfoView>();
            users.ForEach(
                          u =>
                              {
                                  var user = new UserInfoView
                                  {
                                      FullName = u.FullName,
                                      Id = u.Id,
                                      LoginName = u.UserName,
                                      RoleIds = u.Roles.Select(r => r.RoleId).ToList(),
                                      RoleNames = string.Join(
                                                          ",",
                                          roles.Where(r => u.Roles.Select(ur => ur.RoleId).Contains(r.Id)).Select(r => r.Name).ToList())
                                  };
                                  list.Add(user);
                              });
            var pager = new Pager<UserInfoView>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Datas = list,
                Total = list.Count
            };
            pager.GetShowIndex();
            return this.Json(pager, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangedPasswordView changedPassword)
        {
            var result = await this.UserManager.ChangePasswordAsync(
                                                         this.User.Identity.GetUserId(),
                        changedPassword.CurrentPassword,
                        changedPassword.NewPassword);
            return this.Json(result.Succeeded ? new ActionResultData<string>("密码修改成功！") : new ActionResultStatus(10, result.Errors.First()), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="infoView"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admins")]
        public ActionResult CreateUser(UserInfoView infoView)
        {
            try
            {
                var user = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = infoView.LoginName,
                    EmailConfirmed = false,
                    FullName = infoView.FullName,
                    TwoFactorEnabled = true,
                };
                foreach (var roleName in infoView.RoleIds)
                {
                    user.Roles.Add(new IdentityUserRole() { RoleId = roleName, UserId = user.Id });
                }

                this.UserManager.Create(user, "11111111");
                return this.Json(new ActionResultStatus(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return this.Json(new ActionResultStatus(ex), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="infoView"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admins")]
        public ActionResult UpdateUser(UserInfoView infoView)
        {
            try
            {
                var user = this.UserManager.FindByName(infoView.LoginName);
                if (user == null)
                {
                    return this.Json(new ActionResultStatus(10, "用户不存在！"), JsonRequestBehavior.AllowGet);
                }

                user.UserName = infoView.LoginName;
                user.FullName = infoView.FullName;
                user.Roles.Clear();
                foreach (var roleId in infoView.RoleIds)
                {
                    user.Roles.Add(new IdentityUserRole { RoleId = roleId, UserId = user.Id });
                }

                this.UserManager.Update(user);
                return this.Json(new ActionResultStatus(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return this.Json(new ActionResultStatus(ex), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userName">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [Authorize(Roles = "Admins")]
        public ActionResult DeleteUser(string id)
        {
            try
            {
                var user = this.UserManager.FindById(id);
                if (user == null)
                {
                    return this.Json(new ActionResultStatus(10, "用户不存在！"), JsonRequestBehavior.AllowGet);
                }

                this.UserManager.Delete(user);
                return this.Json(new ActionResultStatus(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return this.Json(new ActionResultStatus(ex), JsonRequestBehavior.AllowGet);
            }
        }
    }
}