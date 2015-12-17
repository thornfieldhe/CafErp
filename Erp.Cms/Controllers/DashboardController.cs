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
            var defaultColumn = Article.Get(r => r.Category == Category.Columns).OrderBy(r => r.Order).FirstOrDefault();
            if (defaultColumn == null)
            {
                this.ViewBag.defaultId = Guid.NewGuid();
            }
            else
            {
                this.ViewBag.defaultId = defaultColumn.Id;
            }
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

        public ActionResult GetAllArticles(Guid columnId)
        {
            var column = Article.Find(columnId);
            return this.Json(Article.Get(r => r.LevelCode.StartsWith(column.LevelCode) && r.Id != column.Id).OrderBy(r => r.LevelCode).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadArticle(Guid aritcleId)
        {
            var article = Article.Find(aritcleId);
            return this.Json(new
            {
                Name = article.Name,
                CreatedDate = article.CreatedDate.ToShortDateString(),
                Content = article.Content,
                Category = article.Category
            }, JsonRequestBehavior.AllowGet);
        }
    }
}