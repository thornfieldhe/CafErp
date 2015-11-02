// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Product.cs" company="">
//   
// </copyright>
// <summary>
//   商品
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Erp.Eam.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TAF.Utility;
    using TAF.Validation;

    /// <summary>
    /// 商品
    /// </summary>
    [Table("Products")]
    public partial class Product
    {
        #region 属性

        /// <summary>
        /// 商品编号
        /// </summary>
        [Required]
        public string Code
        {
            get; set;
        }

        /// <summary>
        /// 商品名称
        /// </summary>
        [Required]
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// 商品规格
        /// </summary>
        public string Specification
        {
            get; set;
        }

        /// <summary>
        /// 拼音码
        /// </summary>
        public string ShortName
        {
            get; protected set;
        }

        /// <summary>
        /// 单位
        /// </summary>
        [Required]
        public string Unit
        {
            get; set;
        }

        /// <summary>
        /// 辅助单位
        /// </summary>
        [Required]
        public string Unit2
        {
            get; set;
        }

        /// <summary>
        /// 商品类别Id
        /// </summary>
        [GuidRequired]
        public Guid CategoryId
        {
            get; set;
        }

        [ForeignKey("CategoryId")]
        public virtual ProductCategory Category
        {
            get; set;
        }

        /// <summary>
        /// 商品颜色Id
        /// </summary>
        [GuidRequired]
        public Guid ColorId
        {
            get; set;
        }

        [ForeignKey("ColorId")]
        public virtual Info Color
        {
            get; set;
        }


        /// <summary>
        /// 单位换算
        /// </summary>
        [Required]
        [Min]
        public decimal Conversion
        {
            get; set;
        }

        #endregion

        #region 覆写基类方法

        protected override void PreInsert()
        {
            CreateShotName();
        }

        protected override void PreUpdate()
        {
            CreateShotName();
        }

        #endregion

        #region 私有方法

        private void CreateShotName()
        {
            Name = Name.Trim();
            Code = Code.Trim();
            ShortName = Name.GetChineseSpell();
            if (Product.Exist(r => r.Code == Code && r.Id != Id))
            {
                AddValidationRule(new EmptyErrorValidateionRule("产品编号已存在！"));
            }
        }

        #endregion
    }
}