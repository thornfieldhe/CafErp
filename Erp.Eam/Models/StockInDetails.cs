// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StockInDetails.cs" company="">
//   
// </copyright>
// <summary>
//   入库单明细
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Erp.Eam.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("StockInDetails")]
    public partial class StockInDetail
    {
        /// <summary>
        /// 产品Id
        /// </summary>
        public Guid ProductId
        {
            get; set;
        }

        /// <summary>
        /// 入库量
        /// </summary>
        public decimal Amount
        {
            get; set;
        }

        /// <summary>
        /// 入库单编号
        /// </summary>
        public Guid StockInId
        {
            get; set;
        }

        /// <summary>
        /// 仓库
        /// </summary>
        public string Store
        {
            get; set;
        }
        /// <summary>
        /// 入库单
        /// </summary>
        [ForeignKey("StockInId")]
        public StockIn StockIn
        {
            get; set;
        }
    }
}