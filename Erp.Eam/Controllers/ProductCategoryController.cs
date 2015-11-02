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
    using System.Web.Mvc;

    using Erp.Eam.Models;
    using TAF.Mvc;

    /// <summary>
    /// The manage controller.
    /// </summary>
    [Authorize]
    public class ProductCategoryController : BaseController<ProductCategory, ProductCategoryView>
    {
    }
}