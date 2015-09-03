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
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;

    using Erp.Cms.Business;
    using Erp.Cms.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;

    /// <summary>
    /// The manage controller.
    /// </summary>
    [Authorize]
    public class ManageController : Controller
    {
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

        #region 账户相关

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
                        return this.Json(new ActionResultData<string>("/Manage/Index"), JsonRequestBehavior.AllowGet);
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

        public async Task<ActionResult> ChangePassword(ChangedPasswordView changedPassword)
        {
            var result = await this.UserManager.ChangePasswordAsync(
                                                         this.User.Identity.GetUserId(),
                        changedPassword.CurrentPassword,
                        changedPassword.NewPassword);
            return this.Json(result.Succeeded ? new ActionResultData<string>("密码修改成功！") : new ActionResultStatus(10, "当前密码不正确！"), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 栏目操作

        public ActionResult ColumnIndex()
        {
            return this.PartialView("_ColumnIndex");
        }

        /// <summary>
        /// 新增栏目
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateColumn(ColumnView column)
        {
            try
            {
                var article = new Article() { Category = Category.Columns, Name = column.Name, Order = column.Order };
                article.Create();
                return this.Json(new ActionResultStatus(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return this.Json(new ActionResultStatus(ex), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 更新栏目
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateColumn(ColumnView column)
        {
            try
            {
                var article = Article.Get(column.Id);
                if (article != null)
                {
                    article.Name = column.Name;
                    article.Order = column.Order;
                    article.Save();
                    return this.Json(new ActionResultStatus(), JsonRequestBehavior.AllowGet);
                }

                return this.Json(new ActionResultStatus(10, "栏目不存在！"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return this.Json(new ActionResultStatus(ex), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 获取栏目列表
        /// </summary>
        /// <param name="pageIndex">
        /// The page Index.
        /// </param>
        /// <param name="pageSize">
        /// The page Size.
        /// </param>
        /// <returns>
        /// </returns>
        public ActionResult GetColumnList(int pageIndex, int pageSize = 20)
        {
            var pager = new Pager<Article>() { PageIndex = pageIndex, PageSize = pageSize };
            pager = Article.Pages(pager, r => r.Category == Category.Columns, r => r.Order, true);
            return this.Json(pager, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 统一删除文章，分类及栏目的方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteArticle(Guid id)
        {
            try
            {
                var article = Article.Get(id);
                if (article != null)
                {
                    article.Delete();
                    return this.Json(new ActionResultStatus(), JsonRequestBehavior.AllowGet);
                }

                return this.Json(new ActionResultStatus(10, "对象不存在！"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return this.Json(new ActionResultStatus(ex), JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region 目录操作

        public ActionResult CatalogsIndex()
        {
            return this.PartialView("_CatalogsIndex");
        }

        /// <summary>
        /// 新增目录
        /// </summary>
        /// <param name="catalog"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateCatalog(CatalogView catalog)
        {
            try
            {
                var article = new Article() { Category = Category.Catalog, Name = catalog.Name, Order = catalog.Order, ParentId = catalog.ParentId };
                article.Create();
                return this.Json(new ActionResultStatus(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return this.Json(new ActionResultStatus(ex), JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 更新目录
        /// </summary>
        /// <param name="catalog"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateCatalog(CatalogView catalog)
        {
            try
            {
                var article = Article.Get(catalog.Id);
                if (article != null)
                {
                    article.Name = catalog.Name;
                    article.Order = catalog.Order;
                    article.ParentId = catalog.ParentId;
                    article.Save();
                    return this.Json(new ActionResultStatus(), JsonRequestBehavior.AllowGet);
                }

                return this.Json(new ActionResultStatus(10, "目录不存在！"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return this.Json(new ActionResultStatus(ex), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 获取目录列表
        /// </summary>
        /// <returns>
        /// </returns>
        public ActionResult GetCatalogList()
        {
            var list = Article.Get(r => r.Category == Category.Columns || r.Category == Category.Catalog)
                .OrderBy(r => r.Level).ThenBy(r => r.Order).ToList();
            return this.Json(list, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 文章操作

        public ActionResult ArticlesIndex()
        {
            return this.PartialView("_ArticlesIndex");
        }

        public ActionResult ArticleEdit(Guid? id, Guid? parentId)
        {
            return this.PartialView("_ArticleEdit", id.HasValue ? Article.Get(id.Value) : new Article() { ParentId = parentId });
        }

        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateArticle(ArticleView article)
        {
            try
            {
                var item = new Article() { Category = Category.Articles, Name = article.Name, Order = article.Order, ParentId = article.ParentId, Content = article.Content };
                item.Create();
                return this.Json(new ActionResultStatus(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return this.Json(new ActionResultStatus(ex), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateArtile(ArticleView article)
        {
            try
            {
                var item = Article.Get(article.Id);
                if (item != null)
                {
                    item.Name = article.Name;
                    item.Order = article.Order;
                    item.ParentId = article.ParentId;
                    item.Content = article.Content;
                    item.Save();
                    return this.Json(new ActionResultStatus(), JsonRequestBehavior.AllowGet);
                }

                return this.Json(new ActionResultStatus(10, "文章不存在！"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return this.Json(new ActionResultStatus(ex), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="catalogId">
        /// The catalog Id.
        /// </param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns>
        /// </returns>
        public ActionResult GetArticleList(Guid catalogId, int pageIndex, int pageSize = 20)
        {
            var pager = new Pager<Article>() { PageIndex = pageIndex, PageSize = pageSize };
            pager = Article.Pages(pager, r => r.Category == Category.Articles && r.ParentId == catalogId, r => r.Order, true);

            return this.Json(pager, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 轮播图操作

        public ActionResult UploadImages()
        {
            return this.View();
        }

        public ActionResult CarouselIndex()
        {
            return this.View();
        }

        #endregion
    }
}