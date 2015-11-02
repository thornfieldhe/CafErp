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

    using TAF.Core;

    /// <summary>
    /// The product category view.
    /// </summary>
    public class ProductCategoryView : IEntityBase
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the parent id.
        /// </summary>
        public Guid ParentId
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id
        {
            get; set;
        }
    }
}