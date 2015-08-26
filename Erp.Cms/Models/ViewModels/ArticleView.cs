namespace Erp.Cms.Models
{
    using System;

    public class ColumnView
    {
        public string Name
        {
            get; set;
        }

        public int Order
        {
            get; set;
        }

        public Guid Id
        {
            get; set;
        }

        public string LevelCode
        {
            get; set;
        }
    }

    public class CatalogView : ColumnView
    {
        public Guid ParentId
        {
            get; set;
        }
    }

    public class ArticleView : CatalogView
    {
        public string Content
        {
            get; set;
        }
    }
}