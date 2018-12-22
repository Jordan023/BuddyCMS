using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Buddy.WebApp.Security;

namespace Buddy.WebApp
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session == null) return;
            var currentCulture = (CultureInfo)Session["original_culture"];
            var newCulture = (CultureInfo)Session["culture"];

            var change = false;

            if (newCulture == null)
            {
                var language = "en";

                if (HttpContext.Current.Request.UserLanguages != null &&
                    HttpContext.Current.Request.UserLanguages.Length != 0)
                    language = HttpContext.Current.Request.UserLanguages[0].Substring(0, 2);

                newCulture = new CultureInfo(language);
                Session["culture"] = newCulture;

                change = true;
            }

            Thread.CurrentThread.CurrentUICulture = newCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(newCulture.Name);

            if (currentCulture == null || newCulture == null) return;
            if (change || currentCulture.ThreeLetterISOLanguageName != newCulture.ThreeLetterISOLanguageName)
                User.Refresh();
        }
    }
}
