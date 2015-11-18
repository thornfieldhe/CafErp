// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StockIn.cs" company="">
//   
// </copyright>
// <summary>
//   入库单
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Erp.Eam.Models
{
    using TAF;
    using TAF.Utility;

    /// <summary>
    /// 入库单
    /// </summary>
    public partial class StockIn : EfBusiness<StockIn>
    {
        #region 覆写基类方法

        protected override void AddDescriptions()
        {
            base.AddDescriptions();
            AddDescription("Code:" + Code.ToStr());
            AddDescription("CreatedBy:" + CreatedBy.ToStr());
            AddDescription("ModifyBy:" + ModifyBy.ToStr());
            AddDescription("Store:" + Store.ToStr());
        }
        #endregion
    }
}



