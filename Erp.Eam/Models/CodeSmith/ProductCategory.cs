// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductCategory.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the Article type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Erp.Eam.Models
{

    using TAF;

    /// <summary>
    /// 文章模型
    /// </summary>
    public partial class ProductCategory : EfBusiness<ProductCategory>
    {
        #region 覆写基类方法

        protected override void AddDescriptions()
        {
            base.AddDescriptions();
            AddDescription("Name:" + Name);
            AddDescription("ParentId:" + (ParentId.HasValue ? ParentId.Value.ToString() : string.Empty));
            AddDescription("Level:" + Level);
        }
        #endregion
    }
}