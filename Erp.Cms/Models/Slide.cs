namespace Erp.Cms.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using Business;

    /// <summary>
    /// The slide.
    /// </summary>
    [Table("Slides")]
    public partial class Slide
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName
        {
            get; set;
        }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath
        {
            get; set;
        }

        /// <summary>
        /// 循环频率
        /// </summary>
        public int Rate
        {
            get; set;
        }

        #region 静态方法

        public static void CreateList(IList<Slide> items)
        {
            var contex = ContextWapper.Instance.Context;
            contex.Slides.AddRange(items);
            contex.SaveChanges();
        }

        #endregion
    }
}