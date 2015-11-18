// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StockInViews.cs" company="">
//   
// </copyright>
// <summary>
//   入库单视图
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Erp.Eam.Models
{
    using System;

    using TAF.Core;

    /// <summary>
    /// 列表视图
    /// </summary>
    public class StockInListView : IEntityBase
    {
        public Guid Id
        {
            get; set;
        }

        public string Code
        {
            get; set;
        }

        public string CreatedBy
        {
            get; set;
        }

        public string ModifyBy
        {
            get; set;
        }

        public string Store
        {
            get; set;
        }

    }

    /// <summary>
    /// 对象视图
    /// </summary>
    public class StockInView : IEntityBase
    {
        public Guid Id
        {
            get; set;
        }

        public string Code
        {
            get; set;
        }

        public string Creator
        {
            get; set;
        }

        public string Store
        {
            get; set;
        }

    }
}



