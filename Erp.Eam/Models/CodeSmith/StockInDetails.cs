// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StockInDetail.cs" company="">
//   
// </copyright>
// <summary>
//   入库明细
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Erp.Eam.Models
{
    using TAF;
    using TAF.Utility;

    /// <summary>
    /// 入库明细
    /// </summary>
    public partial class StockInDetail : EfBusiness<StockInDetail>
    {
        #region 覆写基类方法

        protected override void AddDescriptions()
        {
            base.AddDescriptions();
            AddDescription("Amount:" + Amount.ToStr());
        }
        #endregion
    }
}



