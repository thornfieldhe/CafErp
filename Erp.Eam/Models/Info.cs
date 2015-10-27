// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Info.cs" company="">
//   综合信息
// </copyright>
// <summary>
//   Defines the Info type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Erp.Eam.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public enum InfoCategory
    {
        /// <summary>
        /// 单位
        /// </summary>
        Unit = 0,

        /// <summary>
        /// 规格
        /// </summary>
        Specification = 1,

        /// <summary>
        /// 颜色
        /// </summary>
        Color = 2,

        /// <summary>
        /// 品牌
        /// </summary>
        Brand = 3,

        /// <summary>
        /// 仓库
        /// </summary>
        Storehouse = 4
    }

    /// <summary>
    /// 基本信息
    /// </summary>
    [Table("Infos")]
    public partial class Info
    {
        public string Name
        {
            get; set;
        }

        public InfoCategory Category
        {
            get; set;
        }
    }
}