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
    using System;
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
        /// <returns></returns>
        public static List<Tuple<Guid, string>> ToSelectItems(InfoCategory category)
        {
            var result = new List<Tuple<Guid, string>>() { new Tuple<Guid, string>(Guid.Empty, string.Empty) };
            result.AddRange(Info.Get(r => r.Category == category).Select(r => new Tuple<Guid, string>(r.Id, r.Name)).OrderBy(r => r.Item2).ToList());
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