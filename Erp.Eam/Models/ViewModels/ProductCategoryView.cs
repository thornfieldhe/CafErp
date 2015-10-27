// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductCategoryView.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the ProductCategoryView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Erp.Eam.Models
{
    using System;

    using CAF;

    public class ProductCategoryView : IEntityBase
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