// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Info.cs" company="">
//   
// </copyright>
// <summary>
//   The info.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Erp.Eam.Models
{
    using System;

    using Erp.Eam.Business;

    /// <summary>
    /// The info.
    /// </summary>
    public partial class Info : EfBusiness<Info>
    {
        #region 构造函数
        public Info(Guid id) : base(id)
        {
        }

        public Info() : this(Guid.NewGuid())
        {
        }

        #endregion

        #region 覆写基类方法

        protected override void AddDescriptions()
        {
            base.AddDescriptions();
            AddDescription("Name:" + Name);
            AddDescription("Category:" + Category.ToString());
        }
        #endregion
    }
}