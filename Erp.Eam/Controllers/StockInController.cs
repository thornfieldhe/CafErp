// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StockInController.cs" company="">
//   
// </copyright>
// <summary>
//   入库单控制器
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Erp.Eam.Controllers
{
    using System;
    using System.Web.Mvc;
    using Erp.Eam.Models;
    using TAF;
    using TAF.Mvc;
    using TAF.Utility;

    /// <summary>
    /// The manage controller.
    /// </summary>
    public class StockInController : BaseController<StockIn, StockInView, StockInListView>
    {
        public ActionResult List(StockInListView query, int pageIndex, int pageSize = 20)
        {
            Func<StockIn, bool> func = r =>
              (string.IsNullOrWhiteSpace(query.Code) || (!string.IsNullOrWhiteSpace(query.Code) && r.Code.ToStr().ToLower().Contains(query.Code.ToStr().ToLower())))
              && (string.IsNullOrWhiteSpace(query.Store) || (!string.IsNullOrWhiteSpace(query.Store) && r.Store.ToStr() == query.Store.ToStr()));

            return Json(
                        StockIn.Pages(
                                      new Pager<StockInListView>
                                      {
                                          PageIndex = pageIndex,
                                          PageSize = pageSize
                                      },
                            func,
                            r => r.CreatedBy,
                            false),
                JsonRequestBehavior.AllowGet);
        }
    }
}



