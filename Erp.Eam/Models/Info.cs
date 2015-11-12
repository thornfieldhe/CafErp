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
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

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

        /// <summary>
        /// 获取信息名值列表
        /// </summary>
        /// <param name="category"></param>
        /// <param name="allowEmpty"></param>
        /// <returns></returns>
        public static List<string> ToSelectItems(InfoCategory category, bool allowEmpty = true)
        {
            var result = new List<string>();
            if (allowEmpty)
            {
                result.Add(string.Empty);
            }
            result.AddRange(Info.Get(r => r.Category == category).Select(r => r.Name).OrderBy(r => r).ToList());
            return result;
        }

        #region 覆写基类方法

        protected override void PreInsert()
        {
            Name = Name.Trim();
        }

        protected override void PreUpdate()
        {
            Name = Name.Trim();
        }

        #endregion

    }
}