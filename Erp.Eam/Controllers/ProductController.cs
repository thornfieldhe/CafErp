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

    /// <summary>
    /// The manage controller.
    /// </summary>
    [Authorize]
    public class ProductController : BaseController<Product, ProductView, ProductListView>
    {
        public ActionResult List(ProductListView query, int pageIndex, int pageSize = 20)
        {
            Func<Product, bool> func = r => r.CategoryId == query.CategoryId || r.Color == query.Color || r.Unit == query.Unit
                                 || r.Unit2 == query.Unit
                                 || (!string.IsNullOrWhiteSpace(query.Name) && (r.Name.Contains(query.Name.Trim()) || r.ShortName.Contains(query.Name.Trim().ToLower())))
                                 || (!string.IsNullOrEmpty(query.Code) && r.Code.ToLower().Contains(query.Code.Trim().ToLower()));

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