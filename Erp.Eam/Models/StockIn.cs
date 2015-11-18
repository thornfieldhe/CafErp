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
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using Business;

    using Microsoft.AspNet.Identity;

    [Table("StockIns")]
    public partial class StockIn
    {
        #region 属性

        /// <summary>
        /// 单据编号
        /// </summary>
        public string Code
        {
            get; set;
        }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreatedBy
        {
            get; set;
        }


        /// <summary>
        /// 创建者
        /// </summary>
        [NotMapped]
        public ApplicationUser Creator
        {
            get
            {
                return ApplicationUserManager.CreateForEF(EFDbContext.Create()).FindById(CreatedBy);
            }
        }

        /// <summary>
        /// 更新者
        /// </summary>
        [NotMapped]
        public string ModifyBy
        {
            get; set;
        }


        /// <summary>
        /// 更新者
        /// </summary>
        public ApplicationUser Updater
        {
            get
            {
                return ApplicationUserManager.CreateForEF(EFDbContext.Create()).FindById(ModifyBy);
            }
        }

        /// <summary>
        /// 仓库
        /// </summary>
        public string Store
        {
            get; set;
        }

        /// <summary>
        /// 入库明细
        /// </summary>
        public virtual List<StockInDetail> Details
        {
            get; set;
        }

        #endregion

        #region 重载

        protected override void PreInsert()
        {
            var stocks = new List<Stock>();
            this.Details.ForEach(
                                 s =>
                                     {
                                         var stock = this.DbContex.Set<Stock>().FirstOrDefault(r => r.ProductId == s.ProductId && r.Storehouse == this.Store);
                                         if (stock == null)
                                         {
                                             stock = new Stock
                                             {
                                                 Amount = s.Amount,
                                                 ProductId = s.ProductId,
                                                 Storehouse = Store
                                             };
                                             stocks.Add(stock);
                                         }
                                         else
                                         {
                                             stock.Amount += s.Amount;
                                         }
                                     });
            this.DbContex.Set<Stock>().AddRange(stocks);
            base.PreInsert();
        }

        #endregion

    }
}