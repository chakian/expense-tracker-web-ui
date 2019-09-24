using ExpenseTracker.WebUI.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ExpenseTracker.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest()
        {
            //TODO: how do we get the route data here
            //string lang = (string)filterContext.RouteData.Values["lang"];
            
            string cultureCode = string.Empty;
            var cookie = Context.Request.Cookies["culture"];
            if (cookie == null)
            {
            }

            //var culture = new CultureInfo(cookie.Value);
            LanguageHelper.SetLanguage(cultureCode);
        }
    }
}
