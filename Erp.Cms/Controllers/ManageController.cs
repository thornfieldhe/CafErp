// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManageController.cs" company="">
//   
// </copyright>
// <summary>
//   The manage controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using System.Web.Mvc;

namespace Erp.Cms.Controllers
{
    using System.Threading.Tasks;
    using System.Web;

    using Erp.Cms.Models;

    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;

    /// <summary>
    /// The manage controller.
    /// </summary>
    [Authorize]
    public class ManageController : Controller
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
        /// 
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
        /// Initializes a new instance of the <see cref="ManageController"/> class.
        /// </summary>
        /// <param name="userManager">
        /// The user manager.
        /// </param>
        /// <param name="signInManager">
        /// The sign in manager.
        /// </param>
        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
        }

        public ManageController()
        {
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
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            return this.View();
        }


        [AllowAnonymous]
        public ActionResult Login()
        {
            return this.View();
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
            var result = await this.SignInManager.PasswordSignInAsync(user.Name, user.Password, true, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    return this.Json(new ActionResultData<string>("/Manage/Index"), JsonRequestBehavior.AllowGet);
                default:
                    return this.Json(new ActionResultStatus(10, "用户名或密码错误"), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SignOut()
        {
            this.AuthenticationManager.SignOut();
            return this.View("Login");
        }
    }
}