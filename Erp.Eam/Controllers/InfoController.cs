// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InfoController.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the InfoController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Erp.Eam.Controllers
{
    using System.Web.Mvc;
    using Erp.Eam.Models;
    using TAF;
    using TAF.Mvc;

    public class InfoController : BaseController<Info, InfoView, InfoView>
    {
        /// <summary>
        /// 获取信息列表
        /// </summary>
        /// <param name="category">
        /// The category.
        /// </param>
        /// <param name="pageIndex">
        /// The page Index.
        /// </param>
        /// <param name="pageSize">
        /// The page Size.
        /// </param>
        /// <returns>
        /// </returns>
        public ActionResult GetInfoList(InfoCategory category, int pageIndex, int pageSize = 20)
        {
            var pager = Info.Pages(new Pager<InfoView>() { PageIndex = pageIndex, PageSize = pageSize }, r => r.Category == category, r => r.Name);
            return this.Json(pager, JsonRequestBehavior.AllowGet);
        }
    }
}


