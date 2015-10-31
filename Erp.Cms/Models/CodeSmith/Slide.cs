namespace Erp.Cms.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using Business;

    using TAF;

    /// <summary>
    /// The slide.
    /// </summary>
    public partial class Slide : EfBusiness<Slide>
    {
        #region 构造函数

        public Slide(Guid id) : base(id)
        {
        }

        public Slide() : this(Guid.NewGuid())
        {
        }

        #endregion

        #region 覆写基类方法

        protected override void AddDescriptions()
        {
            base.AddDescriptions();
            this.AddDescription("FileName:" + this.FileName);
            this.AddDescription("FilePath:" + this.FilePath);
            this.AddDescription("Rate:" + this.Rate);
        }
        #endregion
    }
}