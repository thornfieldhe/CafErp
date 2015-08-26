namespace Erp.Cms.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Erp.Cms.Business;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.ContextKey = "DefaultConnection";
        }
    }


    public class DbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        public const string SaUserId = "76edf148-3e31-4e9e-8cf8-f17d3c96f05f";

        public const string SaUserName = "sa@admins.com";

        public const string SaEmail = "sa@admins.com";

        public const string SaPassword = "11111111";

        public const string SaRoleAdmins = "Admins";

        public const string SaRoleUsers = "Users";

        protected override void Seed(ApplicationDbContext context)
        {
            base.Seed(context);
            const string fullName = "系统管理员";
            var roleNames = new string[] { SaRoleAdmins, SaRoleUsers };
            using (var roleManager = ApplicationRoleManager.CreateForEF(context))
            {
                if ((from item in roleNames
                     let role = roleManager.FindByName(item)
                     where role == null
                     select new IdentityRole(item)
                     into role
                     select roleManager.Create(role)).Any(roleresult => !roleresult.Succeeded))
                {
                    throw new Exception("初始化系统失败！");
                }
            }

            using (var userManager = ApplicationUserManager.CreateForEF(context))
            {
                var user = userManager.FindByName(SaUserName);
                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        Id = SaUserId,
                        UserName = SaUserName,
                        Email = SaEmail,
                        EmailConfirmed = false,
                        FullName = fullName,
                        TwoFactorEnabled = true,
                    };
                    userManager.Create(user, SaPassword);
                    userManager.SetLockoutEnabled(user.Id, true);
                }

                var rolesForUser = userManager.GetRoles(user.Id);
                if (!rolesForUser.Contains(SaRoleAdmins))
                {
                    userManager.AddToRole(user.Id, SaRoleAdmins);
                }
            }
        }
    }
}

