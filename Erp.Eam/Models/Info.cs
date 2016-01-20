// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Info.cs" company="">
//  
// </copyright>
// <summary>
//    综合信息
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Erp.Eam.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using TAF.Utility;

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
    [System.ComponentModel.Description("综合信息")]
    public partial class Info
    {
        /// <summary>
        /// 名称
        /// </summary>
        [System.ComponentModel.Description("名称")]
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// 分类
        /// </summary>
        [Description("分类")]
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
            result.AddRange(Get(r => r.Category == category).Select(r => r.Name).OrderBy(r => r).ToList());
            return result;
        }

        #region 覆写基类方法

        protected override void PreInsert()
        {
            Name = Name.ToStr().Trim();
        }

        protected override void PreUpdate()
        {
            Name = Name.ToStr().Trim();
        }

        #endregion

    }
}