// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManageController.cs" company="">
//   
// </copyright>
// <summary>
//   The manage controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using System.Web.Mvc;
namespace Erp.Eam.Controllers
{
    using System;
    using System.Linq;

    using CAF;
    using Erp.Eam.Models;

    /// <summary>
    /// The manage controller.
    /// </summary>
    [Authorize]
    public class ProductController : Controller
    {
        public ProductController()
        {
        }

        #region 产品分类操作

        /// <summary>
        ///删除分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteCategory(Guid id)
        {
            try
            {
                var category = ProductCategory.Get(id);
                if (category != null)
                {
                    category.Delete();
                    return this.Json(new ActionResultStatus(), JsonRequestBehavior.AllowGet);
                }

                return this.Json(new ActionResultStatus(10, "对象不存在！"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return this.Json(new ActionResultStatus(ex), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CatalogsIndex()
        {
            return this.PartialView("_CatalogsIndex");
        }

        /// <summary>
        /// 新增分类
        /// </summary>
        /// <param name="catalog"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateCatalog(CatalogView catalog)
        {
            try
            {
                var category = new ProductCategory() { Name = catalog.Name, Order = catalog.Order, ParentId = catalog.ParentId };
                category.Create();
                return this.Json(new ActionResultStatus(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return this.Json(new ActionResultStatus(ex), JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 更新分类
        /// </summary>
        /// <param name="catalog"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateCatalog(CatalogView catalog)
        {
            try
            {
                var category = ProductCategory.Get(catalog.Id);
                if (category != null)
                {
                    category.Name = catalog.Name;
                    category.Order = catalog.Order;
                    category.ParentId = catalog.ParentId;
                    category.Save();
                    return this.Json(new ActionResultStatus(), JsonRequestBehavior.AllowGet);
                }

                return this.Json(new ActionResultStatus(10, "分类不存在！"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return this.Json(new ActionResultStatus(ex), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 获取分类列表
        /// </summary>
        /// <returns>
        /// </returns>
        public ActionResult GetCatalogList()
        {
            var list = ProductCategory.GetAll()
                .OrderBy(r => r.Level).ThenBy(r => r.Order).ToList();
            return this.Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}