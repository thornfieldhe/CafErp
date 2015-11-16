// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductController.cs" company="">
//   
// </copyright>
// <summary>
//   Erp.Eam
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Erp.Eam.Controller
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
    public class ProductController : BaseController<Product, ProductView, ProductListView>
    {
        public ActionResult List(ProductListView query, int pageIndex, int pageSize = 20)
        {
            Func<Product, bool> func = r =>
               (string.IsNullOrWhiteSpace(query.Code) || (!string.IsNullOrWhiteSpace(query.Code) && r.Code.ToStr() == query.Code.ToStr()))
  && (string.IsNullOrWhiteSpace(query.Name) || (!string.IsNullOrWhiteSpace(query.Name) && r.Name.ToStr() == query.Name.ToStr()))
  && (string.IsNullOrWhiteSpace(query.Unit) || (!string.IsNullOrWhiteSpace(query.Unit) && r.Unit.ToStr() == query.Unit.ToStr()))
  && (string.IsNullOrWhiteSpace(query.Category) || (!string.IsNullOrWhiteSpace(query.Category) && r.CategoryId.ToStr() == query.Category.ToStr()))
  && (string.IsNullOrWhiteSpace(query.Color) || (!string.IsNullOrWhiteSpace(query.Color) && r.Color.ToStr() == query.Color.ToStr()));

            return Json(Product.Pages(new Pager<ProductListView>
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            },
                                      func,
                                      r => r.Name),
                    JsonRequestBehavior.AllowGet);
        }
    }
}



