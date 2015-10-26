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

    public class InfoView
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