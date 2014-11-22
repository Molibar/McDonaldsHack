using System.Web.Http.Filters;

namespace McDonaldsHack.Api.Controllers
{
    public class AcceptCorsAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
            actionExecutedContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }
}