namespace Erp.Eam.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Business;

    using CAF.Utility;

    using WebGrease.Css.Extensions;

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

        /// <summary>
        /// 幻灯片随机排序
        /// </summary>
        /// <returns></returns>
        public static string[] RandomImages()
        {
            var slides =
                ContextWapper.Instance.Context.Slides.Where(r => r.Rate > 0)
                    .Select(r => new
                    {
                        Url = r.FilePath,
                        Rate = r.Rate
                    });
            var list = new List<string>();
            slides.ForEach(r =>
            {
                for (var i = 0; i < r.Rate; i++)
                {
                    list.Add(r.Url);
                }
            });
            var result = list.ToArray();
            Randoms.GetRandomArray(result);
            return result;
        }

        #endregion
    }
}