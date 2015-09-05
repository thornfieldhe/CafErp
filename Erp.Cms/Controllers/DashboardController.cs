using System.Web.Mvc;

namespace Erp.Cms.Controllers
{
    using Erp.Cms.Models;

    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// 轮播图页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Slides()
        {
            return this.View(Slide.RandomImages());
        }
    }
}