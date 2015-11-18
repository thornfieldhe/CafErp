// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StockController.cs" company="">
//   
// </copyright>
// <summary>
//   库存控制器
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
    public class StockController : BaseController<Stock, StockView, StockListView>
    {
        public ActionResult List(StockListView query, int pageIndex, int pageSize = 20)
        {
            Func<Stock, bool> func = r =>
              (string.IsNullOrWhiteSpace(query.Storehouse) || (!string.IsNullOrWhiteSpace(query.Storehouse) && r.Storehouse.ToStr() == query.Storehouse.ToStr()))
              && (string.IsNullOrWhiteSpace(query.Product) || (!string.IsNullOrWhiteSpace(query.Product) && r.ProductId.ToStr() == query.Product.ToStr()))
              && ((query.Amount == 0) || (query.Amount != 0) && r.Amount == query.Amount);

            return Json(Stock.Pages(
                              new Pager<StockListView>
                              {
                                  PageIndex = pageIndex,
                                  PageSize = pageSize
                              },
                              func,
                              r => r.CreatedDate,
                              false),
                    JsonRequestBehavior.AllowGet);
        }
    }
}



