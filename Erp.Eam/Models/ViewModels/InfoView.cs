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
    using System.ComponentModel;
    using TAF.Core;

    [Description("信息")]
    public class InfoView : IEntityBase
    {
        [Description("键")]
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