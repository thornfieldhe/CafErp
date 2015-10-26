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

        protected override void OnException(ExceptionContext filterContext)
        {
            throw filterContext.Exception;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }

        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }
    }
}