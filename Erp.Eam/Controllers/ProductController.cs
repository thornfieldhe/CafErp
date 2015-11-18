// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManageController.cs" company="">
//   
// </copyright>
// <summary>
//   The manage controller.
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
    public class ProductController : BaseController<Product, ProductView, ProductListView>
    {
        public ActionResult List(ProductListView query, int pageIndex, int pageSize = 20)
        {
            Func<Product, bool> func = r =>
            (string.IsNullOrWhiteSpace(query.Category) || (!string.IsNullOrWhiteSpace(query.Category) && r.CategoryId.ToStr() == query.Category.ToStr()))
            && (string.IsNullOrWhiteSpace(query.Color) || r.Color == query.Color.ToStr())
            && (string.IsNullOrWhiteSpace(query.Specification) || r.Specification == query.Specification.ToStr())
            && (string.IsNullOrWhiteSpace(query.Unit.ToStr()) || (r.Unit == query.Unit || r.Unit2 == query.Unit.ToStr()))
            && (string.IsNullOrWhiteSpace(query.Name) || r.Name.Contains(query.Name.ToStr().Trim()) || r.ShortName.Contains(query.Name.ToStr().Trim().ToLower()))
            && (string.IsNullOrWhiteSpace(query.Code) || r.Code.ToLower().Contains(query.Code.ToStr().Trim().ToLower()));

            return Json(
                        Product.Pages(
                                      new Pager<ProductListView>
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