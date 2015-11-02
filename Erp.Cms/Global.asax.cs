using System.Web.Mvc;
using System.Web.Routing;

namespace Erp.Cms
{
    using System.Data.Entity;
    using System.Reflection;
    using System.Web.Optimization;

    using AutoMapper;
    using Business;

    using Erp.Cms.Models;

    using TAF;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer(new Erp.Cms.Migrations.DbInitializer());
            Ioc.RegisterMvc(Assembly.GetExecutingAssembly(), new IocConfig());
            this.InitMap();
        }

        private void InitMap()
        {
            Mapper.CreateMap<Article, ColumnView>();
            Mapper.CreateMap<Article, ArticleView>();
            Mapper.CreateMap<Article, CatalogView>();
            Mapper.CreateMap<Slide, SlideView>();
        }
    }
}
