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
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using CAF;


    /// <summary>
    /// 文章模型
    /// </summary>
    [Table("Articles")]
    public partial class ProductCategory
    {
        #region 属性

        /// <summary>
        /// 标题
        /// </summary>
        [Required(ErrorMessage = "标题不允许为空")]
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order
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
            get; set;
        }

        /// <summary>
        /// 层级编码
        /// </summary>
        public string LevelCode
        {
            get; set;
        }

        #endregion

        #region 覆写基类方法

        protected override void PreInsert()
        {
            this.LevelCode = this.GetMaxLevelCode();
        }

        protected override void PreUpdate()
        {
            if (this.IsLevelChanged())
            {
                var level = this.GetMaxLevelCode();
                var articles =
                    this.DbContex.Set<ProductCategory>().Where(r => r.LevelCode.StartsWith(this.LevelCode) && r.Level != this.Level);
                articles.ForEach(
                                 r =>
                                     {
                                         r.LevelCode = level + r.LevelCode.Substring(this.LevelCode.Length, r.LevelCode.Length - this.LevelCode.Length);
                                         r.Level = r.LevelCode.Length / 2;
                                     });
                this.LevelCode = level;
                this.Level = this.LevelCode.Length / 2;
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
            var maxItem = this.DbContex.Set<ProductCategory>().OrderByDescending(r => r.LevelCode).FirstOrDefault(r => r.ParentId == this.ParentId);

            //当前层级没有项目
            if (maxItem == null)
            {
                var parent = this.DbContex.Set<ProductCategory>().FirstOrDefault(r => r.Id == this.ParentId);

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
            var entry = this.DbContex.Entry(this);
            var current = entry.CurrentValues["ParentId"];
            var orginal = entry.OriginalValues["ParentId"];
            return current != orginal;
        }

        #endregion
    }
}