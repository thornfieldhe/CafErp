namespace Erp.Eam.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Stocks")]
    public partial class Stock
    {
        #region 属性

        /// <summary>
        /// 仓库
        /// </summary>
        public string Storehouse
        {
            get; set;
        }

        /// <summary>
        /// 产品Id
        /// </summary>
        public Guid ProductId
        {
            get; set;
        }

        /// <summary>
        /// 产品
        /// </summary>
        [ForeignKey("ProductId")]
        public Product Product
        {
            get; set;
        }

        /// <summary>
        /// 库存量
        /// </summary>
        public decimal Amount
        {
            get; set;
        }

        #endregion
    }
}