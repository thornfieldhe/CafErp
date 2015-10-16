using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Erp.Eam.Startup))]
namespace Erp.Eam
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}