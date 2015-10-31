// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbContext.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the ApplicationUser type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Erp.Eam.Business
{
    using System.Data.Entity;

    using Erp.Eam.Models;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class DbContext : IdentityDbContext<ApplicationUser>
    {
        public DbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            
        }

        public DbSet<ProductCategory> ProductCategories
        {
            get; set;
        }

        public DbSet<Info> Infos
        {
            get; set;
        }

        public static DbContext Create()
        {
            
            return new DbContext();
        }
    }
}