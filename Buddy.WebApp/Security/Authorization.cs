using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Buddy.WebApp.Security
{
    public class Authorization : AuthorizeAttribute
    {
        public Authorization()
        {

        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated == false)
                return false;
            
            var user = httpContext.User.Identity.Name;

            var routeData = httpContext.Request.RequestContext.RouteData;
            var controller = routeData.GetRequiredString("controller").Replace("Controller", "");
            var action = routeData.GetRequiredString("action");

            if (httpContext.User.SecurityRights() != null)
            {
                controller = controller.ToLower();
                action = action.ToLower();

                return true;
                //return httpContext.User.SecurityRights().Any(s => s.controller.ToLower() == controller && s.action.ToLower() == action);
            }
            else if (false) // invalid user
            {
                return false;
            }

            return true;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("~/Login/");
                return;
            }

            if (filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "NotAuthorized", ex = new UnauthorizedAccessException() }));
            }

        }

    }
}