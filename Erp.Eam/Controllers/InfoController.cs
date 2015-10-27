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
    using System;
    using System.Web.Mvc;

    using Erp.Eam.Business;
    using Erp.Eam.Models;

    public class InfoController : BaseController<Info, InfoView>
    {
        /// <summary>
        /// 获取分类列表
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
            var pager = new Pager<InfoView>() { PageIndex = pageIndex, PageSize = pageSize };
            pager = Info.Pages(pager, r => r.Category == category, r => r.Name, r => new InfoView() { Name = r.Name, Category = r.Category, Id = r.Id }, true);
            return this.Json(pager, JsonRequestBehavior.AllowGet);
        }
    }
}


