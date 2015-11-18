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
    }
}