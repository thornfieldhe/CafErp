using System.Web.Mvc;

namespace Erp.Cms.Controllers
{
    using System;
    using System.Linq;

    using Erp.Cms.Models;

    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return this.View(Article.Get(r => r.Category == Category.Columns).OrderBy(r => r.Order).ToList());
        }

        /// <summary>
        /// 轮播图页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Slides()
        {
            return this.View(Slide.RandomImages());
        }

        public ActionResult GetAllArticles()
        {
            return this.Json(Article.GetAll(true).OrderBy(r => r.LevelCode), JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadArticle(Guid aritcleId)
        {
            return	this.Json(Article.Get(aritcleId).Content,JsonRequestBehavior.AllowGet) ; 
        }
    }
}