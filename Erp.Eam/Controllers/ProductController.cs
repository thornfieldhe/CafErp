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
        /// 删除分类
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

                return this.Json(new ActionResultStatus(10, "商品分类不存在！"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return this.Json(new ActionResultStatus(ex), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CategoryIndex()
        {
            return this.PartialView("_CategoryIndex");
        }

        /// <summary>
        /// 新增分类
        /// </summary>
        /// <param name="categoryView"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateCategory(ProductCategoryView categoryView)
        {
            if (categoryView == null)
            {
                throw new ArgumentNullException("categoryView");
            }
            try
            {
                var category = new ProductCategory() { Name = categoryView.Name, ParentId = categoryView.ParentId };
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
        /// <param name="categoryView"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateCategory(ProductCategoryView categoryView)
        {
            try
            {
                var category = ProductCategory.Get(categoryView.Id);
                if (category != null)
                {
                    category.Name = categoryView.Name;
                    category.ParentId = categoryView.ParentId;
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
        public ActionResult GetCategoryList()
        {
            var list = ProductCategory.GetAll()
               .OrderBy(r => r.Level).ThenBy(r => r.Name).ToList();
            return this.Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}