// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Article.cs" company="">
//   
// </copyright>
// <summary>
//   文章模型
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Erp.Eam.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using TAF.Utility;

    /// <summary>
    /// 产品分类
    /// </summary>
    [Table("ProductCategories")]
    public partial class ProductCategory
    {
        #region 属性

        /// <summary>
        /// 分类名称
        /// </summary>
        [Required(ErrorMessage = "分类名称不允许为空")]
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// 父节点
        /// </summary>
        public Guid? ParentId
        {
            get; set;
        }

        /// <summary>
        /// 层级
        /// </summary>
        public int Level
        {
            get; protected set;
        }

        /// <summary>
        /// 层级编码
        /// </summary>
        public string LevelCode
        {
            get; protected set;
        }

        #endregion

        public static IList<Tuple<Guid, string>> ToSelectItems()
        {
            var result = new List<Tuple<Guid, string>>() { new Tuple<Guid, string>(Guid.Empty, "请选择...") };
            result.AddRange(ProductCategory.GetAll().OrderBy(r => r.LevelCode).Select(r => new Tuple<Guid, string>(r.Id, "|" + "-".Repeat(r.Level * 3) + r.Name)).ToList());
            return result;
        }

        #region 覆写基类方法

        protected override void PreInsert()
        {
            Name = Name.Trim();
            LevelCode = GetMaxLevelCode();
            Level = LevelCode.Length / 2 - 1;
        }

        protected override void PreUpdate()
        {
            Name = Name.Trim();
            if (IsLevelChanged())
            {
                var level = GetMaxLevelCode();
                var articles =
                    DbContex.Set<ProductCategory>().Where(r => r.LevelCode.StartsWith(LevelCode) && r.Level != Level);
                articles.ForEach(
                                 r =>
                                     {
                                         r.LevelCode = level + r.LevelCode.Substring(LevelCode.Length, r.LevelCode.Length - LevelCode.Length);
                                         r.Level = r.LevelCode.Length / 2 - 1;
                                     });
                LevelCode = level;
                Level = LevelCode.Length / 2 - 1;
            }
        }

        #endregion

        #region 私有方法 

        /// <summary>
        /// 获取当前文章最新层级
        /// </summary>
        /// <returns></returns>
        private string GetMaxLevelCode()
        {
            var maxItem = DbContex.Set<ProductCategory>().OrderByDescending(r => r.LevelCode).FirstOrDefault(r => r.ParentId == ParentId);

            //当前层级没有项目
            if (maxItem == null)
            {
                var parent = DbContex.Set<ProductCategory>().FirstOrDefault(r => r.Id == ParentId);

                //父层级不存在,即为第一条数据;父级存在，即为父级下第一条数据
                return parent == null ? "01" : string.Format("{0}{1}", parent.LevelCode, "01");
            }
            else
            {
                //当前层级最大编号>=9,层级编号直接+1
                if (maxItem.LevelCode.Trim('0').Length % 2 == 0 || maxItem.LevelCode.ToInt() == 9)
                {
                    return (maxItem.LevelCode.ToInt() + 1).ToString();
                }
                return (maxItem.LevelCode.ToInt() + 1).ToString().PadLeft(maxItem.LevelCode.Length, '0');
            }
        }

        /// <summary>
        /// 判断父节点是否改变
        /// </summary>
        /// <returns></returns>
        private bool IsLevelChanged()
        {
            var entry = DbContex.Entry(this);
            return entry.CurrentValues["ParentId"] != entry.OriginalValues["ParentId"];
        }

        #endregion
    }
}