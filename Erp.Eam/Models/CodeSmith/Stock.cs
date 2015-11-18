// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Stock.cs" company="">
//   
// </copyright>
// <summary>
//   库存
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Erp.Eam.Models
{
    using TAF;
    using TAF.Utility;

    /// <summary>
    /// 库存
    /// </summary>
    public partial class Stock : EfBusiness<Stock>
    {
        #region 覆写基类方法

        protected override void AddDescriptions()
        {
            base.AddDescriptions();
            AddDescription("Storehouse:" + Storehouse.ToStr());
            AddDescription("Amount:" + Amount.ToStr());
        }
        #endregion
    }
}
