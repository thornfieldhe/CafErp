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

    using TAF;

    /// <summary>
    /// 文章模型
    /// </summary>
    public partial class Article : EfBusiness<Article>
    {
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