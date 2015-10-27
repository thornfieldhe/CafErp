// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InfoView.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the InfoView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Erp.Eam.Models
{
    using System;
    using CAF;

    public class InfoView : IEntityBase
    {
        public Guid Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public InfoCategory Category
        {
            get; set;
        }
    }
}