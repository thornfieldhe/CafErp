using System.Web.Mvc;
using System.Web.Routing;

namespace Erp.Eam
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Reflection;
    using System.Web.Optimization;

    using AutoMapper;

    using Erp.Eam.Business;
    using Erp.Eam.Models;
    using TAF;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer(new Erp.Eam.Migrations.DbInitializer());
            Ioc.RegisterMvc(Assembly.GetExecutingAssembly(), new IocConfig());
            InitMap();
        }

        private void InitMap()
        {
            Mapper.CreateMap<Info, InfoView>();
            Mapper.CreateMap<InfoView, Info>().ForMember(n => n.Id, m => m.MapFrom(r => r.Id == Guid.Empty ? Guid.NewGuid() : r.Id));

            Mapper.CreateMap<ProductCategory, ProductCategoryView>();
            Mapper.CreateMap<ProductCategoryView, ProductCategory>().ForMember(n => n.Id, m => m.MapFrom(r => r.Id == Guid.Empty ? Guid.NewGuid() : r.Id));
        }
    }
}
