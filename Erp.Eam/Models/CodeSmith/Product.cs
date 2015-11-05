// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Product.cs" company="">
//   
// </copyright>
// <summary>
//   商品
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Erp.Eam.Models
{
    using TAF;

    /// <summary>
    /// 商品
    /// </summary>
    public partial class Product : EfBusiness<Product>
    {
        #region 覆写基类方法

        /// <summary>
        /// The add descriptions.
        /// </summary>
        protected override void AddDescriptions()
        {
            base.AddDescriptions();
            this.AddDescription("Name:" + this.Name);
            this.AddDescription("Code:" + this.Code);
            this.AddDescription("ShortName:" + this.ShortName);
            this.AddDescription("Specification:" + this.Specification);
            this.AddDescription("Unit:" + this.Unit);
            this.AddDescription("Unit2:" + this.Unit2);
            this.AddDescription("Conversion:" + this.Conversion);
            this.AddDescription("CategoryId:" + this.CategoryId);
            this.AddDescription("Color:" + this.Color);
        }

        #endregion
    }
}