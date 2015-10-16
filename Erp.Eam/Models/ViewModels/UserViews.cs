namespace Erp.Eam.Models
{
    public class LoginUser
    {
        public string Name
        {
            get; set;
        }

        public string Password
        {
            get; set;
        }
    }

    public class ConfirmPassword
    {
        public string NewPassword
        {
            get; set;
        }

        public string OldPassword
        {
            get; set;
        }

        public string UserName
        {
            get; set;
        }
    }
}