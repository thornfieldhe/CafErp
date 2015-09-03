// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChangedPasswordView.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the ChangedPasswordView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Erp.Cms.Models
{
    public class ChangedPasswordView
    {
        public string CurrentPassword
        {
            get; set;
        }

        public string NewPassword
        {
            get; set;
        }
    }
}