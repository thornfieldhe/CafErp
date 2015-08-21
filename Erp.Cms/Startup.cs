using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Erp.Cms.Startup))]
namespace Erp.Cms
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
