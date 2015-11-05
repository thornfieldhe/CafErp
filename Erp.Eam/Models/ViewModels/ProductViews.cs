// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Product.cs" company="">
//   
// </copyright>
// <summary>
//   商品
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Erp.Eam.Models
{
    using System;

    using TAF.Core;

    /// <summary>
    /// The product view.
    /// </summary>
    public class ProductView : IEntityBase
    {
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        public string Code
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the specification.
        /// </summary>
        public string Specification
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the short name.
        /// </summary>
        public string ShortName
        {
            get; protected set;
        }

        /// <summary>
        /// Gets or sets the unit.
        /// </summary>
        public string Unit
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the unit 2.
        /// </summary>
        public string Unit2
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the category id.
        /// </summary>
        public Guid CategoryId
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the color id.
        /// </summary>
        public Guid ColorId
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the conversion.
        /// </summary>
        public decimal Conversion
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the note.
        /// </summary>
        public string Note
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

    /// <summary>
    /// The product view.
    /// </summary>
    public class ProductListView : IEntityBase
    {
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        public string Code
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the unit.
        /// </summary>
        public string Unit
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the category name.
        /// </summary>
        public string CategoryName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the unit.
        /// </summary>
        public Guid CategoryId
        {
            get; set;
        }

        public string Color
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