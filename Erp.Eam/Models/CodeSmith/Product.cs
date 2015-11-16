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
    using TAF.Utility;

    /// <summary>
    /// 商品
    /// </summary>
    public partial class Product : EfBusiness<Product>
    {
        #region 覆写基类方法

        protected override void AddDescriptions()
        {
            base.AddDescriptions();
            AddDescription("Code:" + Code.ToStr());
            AddDescription("Name:" + Name.ToStr());
            AddDescription("Specification:" + Specification.ToStr());
            AddDescription("ShortName:" + ShortName.ToStr());
            AddDescription("Unit:" + Unit.ToStr());
            AddDescription("Unit2:" + Unit2.ToStr());
            AddDescription("Color:" + Color.ToStr());
            AddDescription("Conversion:" + Conversion.ToStr());
        }
        #endregion
    }
}



