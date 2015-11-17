// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="">
//   
// </copyright>
// <summary>
//   The mvc application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Erp.Eam
{
    using System;
    using System.Data.Entity;
    using System.Reflection;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using AutoMapper;

    using Erp.Eam.Business;
    using Erp.Eam.Migrations;
    using Erp.Eam.Models;

    using TAF;
    using TAF.Utility;

    /// <summary>
    /// The mvc application.
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// The application_ start.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer(new DbInitializer());
            Ioc.RegisterMvc(Assembly.GetExecutingAssembly(), new IocConfig());
            InitMap();
        }

        /// <summary>
        /// The init map.
        /// </summary>
        private void InitMap()
        {
            Mapper.CreateMap<Info, InfoView>();
            Mapper.CreateMap<InfoView, Info>()
                .ForMember(n => n.Id, m => m.MapFrom(r => r.Id.IsEmpty() ? Guid.NewGuid() : r.Id));

            Mapper.CreateMap<ProductCategory, ProductCategoryView>();
            Mapper.CreateMap<ProductCategoryView, ProductCategory>()
                .ForMember(n => n.Id, m => m.MapFrom(r => r.Id.IsEmpty() ? Guid.NewGuid() : r.Id));

            Mapper.CreateMap<Product, ProductListView>()
                .ForMember(n => n.Category, m => m.MapFrom(m1 => m1.CategoryId.HasValue ? m1.Category.Name : string.Empty));
            Mapper.CreateMap<ProductView, Product>()
                .ForMember(n => n.Id, m => m.MapFrom(r => r.Id.IsEmpty() ? Guid.NewGuid() : r.Id));
            Mapper.CreateMap<Product, ProductView>();
        }
    }
}