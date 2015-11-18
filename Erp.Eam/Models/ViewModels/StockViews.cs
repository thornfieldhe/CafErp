// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StockViews.cs" company="">
//   
// </copyright>
// <summary>
//   库存视图
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Erp.Eam.Models
{
    using System;

    using TAF.Core;

    /// <summary>
    /// 列表视图
    /// </summary>
    public class StockListView : IEntityBase
    {
        public Guid Id
        {
            get; set;
        }

        public string Storehouse
        {
            get; set;
        }

        public string Product
        {
            get; set;
        }

        public string Code
        {
            get; set;
        }

        public string Specification
        {
            get; set;
        }

        public string Brand
        {
            get; set;
        }

        public string Category
        {
            get; set;
        }

        public string Unit
        {
            get; set;
        }

        public string Color
        {
            get; set;
        }

        public decimal Amount
        {
            get; set;
        }

    }

    /// <summary>
    /// 对象视图
    /// </summary>
    public class StockView : IEntityBase
    {
        public Guid Id
        {
            get; set;
        }

        public string Storehouse
        {
            get; set;
        }

        public Guid ProductId
        {
            get; set;
        }

        public decimal Amount
        {
            get; set;
        }

    }
}



