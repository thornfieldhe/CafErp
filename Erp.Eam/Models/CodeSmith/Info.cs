// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Info.cs" company="">
//   
// </copyright>
// <summary>
//   综合信息
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Erp.Eam.Models
{
    using TAF;
    using TAF.Utility;

    /// <summary>
    /// 综合信息
    /// </summary>
    public partial class Info : EfBusiness<Info>
    {
        #region 覆写基类方法

        protected override void AddDescriptions()
        {
            base.AddDescriptions();
            AddDescription("Name:" + Name.ToStr());
            AddDescription("Category:" + Category.ToStr());
        }
        #endregion
    }
}



