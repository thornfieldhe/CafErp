// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductCategory.cs" company="">
//   
// </copyright>
// <summary>
//   商品分类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Erp.Eam.Models
{
    using TAF;
    using TAF.Utility;

    /// <summary>
    /// 商品分类
    /// </summary>
    public partial class ProductCategory : EfBusiness<ProductCategory>
    {
        #region 覆写基类方法

        protected override void AddDescriptions()
        {
            base.AddDescriptions();
            AddDescription("Name:" + Name.ToStr());
            AddDescription("Level:" + Level.ToStr());
            AddDescription("LevelCode:" + LevelCode.ToStr());
        }
        #endregion
    }
}



