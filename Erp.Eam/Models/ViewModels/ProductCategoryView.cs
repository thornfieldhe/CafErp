namespace Erp.Eam.Models
{
    using System;

    public class ProductCategoryView
    {
        public string Name
        {
            get; set;
        }

        public Guid ParentId
        {
            get; set;
        }

        public Guid Id
        {
            get; set;
        }
    }
}