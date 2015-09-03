namespace Erp.Cms.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

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
    }
}