// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Article.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the Article type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Erp.Cms.Models
{
    using System;

    using Erp.Cms.Business;

    /// <summary>
    /// 文章模型
    /// </summary>
    public partial class Article : EfBusiness<Article>
    {
        #region 构造函数

        public Article(Guid id) : base(id)
        {
        }

        public Article() : this(Guid.NewGuid())
        {
        }

        #endregion

        #region 覆写基类方法

        protected override void AddDescriptions()
        {
            base.AddDescriptions();
            this.AddDescription("Name:" + this.Name);
            this.AddDescription("Order:" + this.Order);
            this.AddDescription("ParentId:" + (this.ParentId.HasValue ? this.ParentId.Value.ToString() : string.Empty));
            this.AddDescription("Level:" + this.Level);
            this.AddDescription("Category:" + this.Category);
        }
        #endregion
    }
}