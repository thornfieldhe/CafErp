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
        /// The unit.
        /// </summary>
        Unit = 0,

        /// <summary>
        /// The specification.
        /// </summary>
        Specification = 1,

        /// <summary>
        /// The color.
        /// </summary>
        Color = 2,

        /// <summary>
        /// The brand.
        /// </summary>
        Brand = 3
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