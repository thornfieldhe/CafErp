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

            Mapper.CreateMap<ProductView, Product>();
            Mapper.CreateMap<Product, ProductView>();
            Mapper.CreateMap<Product, ProductListView>()
            .ForMember(n => n.Code, m => m.MapFrom(m1 => m1.Code.ToStr()))
            .ForMember(n => n.Name, m => m.MapFrom(m1 => m1.Name.ToStr()))
            .ForMember(n => n.Specification, m => m.MapFrom(m1 => m1.Specification.ToStr()))
            .ForMember(n => n.Unit, m => m.MapFrom(m1 => m1.Unit.ToStr()))
            .ForMember(n => n.Category, m => m.MapFrom(m1 => m1.CategoryId.HasValue ? m1.Category.Name.ToStr() : string.Empty)) // 单独处理Guid对象
            .ForMember(n => n.Color, m => m.MapFrom(m1 => m1.Color.ToStr()))
            .ForMember(n => n.Brand, m => m.MapFrom(m1 => m1.Brand.ToStr()));

            Mapper.CreateMap<StockInView, StockIn>();
            Mapper.CreateMap<StockIn, StockInView>();
            Mapper.CreateMap<StockIn, StockInListView>()
                .ForMember(n => n.Code, m => m.MapFrom(m1 => m1.Code.ToStr()))
                .ForMember(n => n.CreatedBy, m => m.MapFrom(m1 => m1.Creator.FullName.ToStr()))
                .ForMember(n => n.ModifyBy, m => m.MapFrom(m1 => m1.Creator.FullName.ToStr()))
                .ForMember(n => n.CreatedAt, m => m.MapFrom(m1 => m1.CreatedDate.ToShortDateString()))
                .ForMember(n => n.Store, m => m.MapFrom(m1 => m1.Store.ToStr()));

            Mapper.CreateMap<StockView, Stock>();
            Mapper.CreateMap<Stock, StockView>();
            Mapper.CreateMap<Stock, StockListView>()
                .ForMember(n => n.Storehouse, m => m.MapFrom(m1 => m1.Storehouse.ToStr()))
                .ForMember(n => n.Product, m => m.MapFrom(m1 => m1.Product.Name))
                .ForMember(n => n.Code, m => m.MapFrom(m1 => m1.Product.Code))
                .ForMember(n => n.Specification, m => m.MapFrom(m1 => m1.Product.Specification))
                .ForMember(n => n.Brand, m => m.MapFrom(m1 => m1.Product.Brand))
                .ForMember(n => n.Category, m => m.MapFrom(m1 => m1.Product.Category.Name))
                .ForMember(n => n.Unit, m => m.MapFrom(m1 => m1.Product.Unit))
                .ForMember(n => n.Color, m => m.MapFrom(m1 => m1.Product.Color))
                .ForMember(n => n.Brand, m => m.MapFrom(m1 => m1.Product.Brand));
        }
    }
}