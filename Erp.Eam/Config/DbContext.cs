﻿// --------------------------------------------------------------------------------------------------------------------
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

    public partial class EFDbContext : IdentityDbContext<ApplicationUser>
    {
        public EFDbContext()
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

        public DbSet<Product> Products
        {
            get; set;
        }

        public DbSet<StockIn> StockIns
        {
            get; set;
        }

        public DbSet<StockInDetail> StockInDetails
        {
            get; set;
        }

        public DbSet<Stock> Stocks
        {
            get; set;
        }

        public static EFDbContext Create()
        {
            return new EFDbContext();
        }
    }
}