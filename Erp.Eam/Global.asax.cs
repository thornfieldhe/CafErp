using System.Web.Mvc;
using System.Web.Routing;

namespace Erp.Eam
{
    using System.Data.Entity;
    using System.Web.Optimization;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer(new Erp.Eam.Migrations.DbInitializer());
            this.InitMap();
        }

        private void InitMap()
        {

        }
    }
}
