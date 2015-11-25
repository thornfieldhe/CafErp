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
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;

    using Erp.Eam.Models;

    using Microsoft.Ajax.Utilities;
    using Microsoft.AspNet.Identity;

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
              && ((query.Stock == 0) || (query.Stock != 0) && r.Amount == query.Stock);

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

        /// <summary>
        /// 根据编码及仓库获取产品库存
        /// </summary>
        /// <param name="code"></param>
        /// <param name="store"></param>
        /// <returns></returns>
        public ActionResult GetProductInStore(string code, string store)
        {
            if (code.IsNullOrWhiteSpace())
            {
                return Json(new ActionResultStatus(10, "请输入编号！"), JsonRequestBehavior.AllowGet);
            }
            if (store.IsNullOrWhiteSpace())
            {
                return Json(new ActionResultStatus(20, "请选择仓库！"), JsonRequestBehavior.AllowGet);
            }
            var product = Product.Get(p => p.Code == code).FirstOrDefault();
            if (product == null)
            {
                return Json(new ActionResultStatus(30, "商品不存在！"), JsonRequestBehavior.AllowGet);
            }
            var stock = Stock.Get(s => s.Product.Code == code && s.Storehouse == store).SingleOrDefault();
            if (stock == null)
            {
                return Json(new ActionResultData<StockChangeListView>(new StockChangeListView
                {
                    Code = code,
                    Stock = 0,
                    Storehouse = store,
                    Product = product.Name,
                    Specification = product.Specification,
                    Brand = product.Brand,
                    Amount = 1,
                    Category = product.Category.Name,
                    Color = product.Color,
                    Units = new Tuple<string, string>(product.Unit, product.Unit2),
                    ProductId = product.Id,
                    Unit = product.Unit,
                    Id = Guid.NewGuid()
                }), JsonRequestBehavior.AllowGet);
            }
            return this.Json(new ActionResultData<StockChangeListView>(Mapper.Map<StockChangeListView>(stock)), JsonRequestBehavior.AllowGet);
        }

    }
}



