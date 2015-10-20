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
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="userManager">
        /// The user manager.
        /// </param>
        /// <param name="signInManager">
        /// The sign in manager.
        /// </param>
        /// <param name="roleManager">
        /// The role Manager 
        /// </param>
        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager roleManager, ApplicationRoleManager roleManager1)
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
            this.RoleManager = roleManager;
            this.roleManager = roleManager1;
        }

        public HomeController()
        {
        }

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
                return this.roleManager ?? ApplicationRoleManager.CreateForEF(null);
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
            return this.PartialView("_UserIndex");
        }

        [Authorize(Roles = "Admins")]
        public ActionResult GetUserList()
        {
            var roles = this.RoleManager.Roles.ToList();
            var list =
                this.UserManager.Users.Select(
                                              u =>
                                              new UserInfo
                                              {
                                                  FullName = u.FullName,
                                                  LoginName = u.FullName,
                                                  Roles = string.Join(
                                                                      ",",
                                                      roles.Where(
                                                                  r => u.Roles.Select(
                                                                                      ur =>
                                                                                                        ur.UserId)
                                                                                             .Contains(u.Id))
                                                          .Select(r => r.Name).ToList())
                                              });

            return this.Json(list, JsonRequestBehavior.AllowGet);
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
        /// <param name="info"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admins")]
        public ActionResult CreateUser(UserInfo info, List<string> roleIds)
        {
            try
            {
                var user = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = info.LoginName,
                    EmailConfirmed = false,
                    FullName = info.FullName,
                    TwoFactorEnabled = true,
                };
                foreach (var roleName in roleIds)
                {
                    user.Roles.Add(new IdentityUserRole() { RoleId = roleName, UserId = user.Id });
                }

                this.UserManager.Create(user, info.Password);
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
        /// <param name="info"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admins")]
        public ActionResult UpdateUser(UserInfo info, List<string> roleIds)
        {
            try
            {
                var user = this.UserManager.FindByName(info.LoginName);
                if (user == null)
                {
                    return this.Json(new ActionResultStatus(10, "用户不存在！"), JsonRequestBehavior.AllowGet);
                }

                user.UserName = info.LoginName;
                user.FullName = info.FullName;
                user.Roles.Clear();
                foreach (var roleId in roleIds)
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
        public ActionResult DeleteUser(string userName)
        {
            try
            {
                var user = this.UserManager.FindByName(userName);
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