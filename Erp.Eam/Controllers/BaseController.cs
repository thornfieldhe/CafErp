using System.Web.Mvc;
namespace Erp.Eam.Controllers
{
    using System.Web.Mvc.Filters;

    public class BaseController : Controller
    {
        protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            base.OnAuthentication(filterContext);
        }
    }
}