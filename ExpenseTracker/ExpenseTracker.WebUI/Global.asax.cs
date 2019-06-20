using System.Globalization;
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
        
        protected void Application_BeginRequest() {
            //var cookie = Context.Request.Cookies["culture"];
            string cookie = "en-US";
            if (cookie != null && !string.IsNullOrEmpty(cookie)) {
                //var culture = new CultureInfo(cookie.Value);
                var culture = new CultureInfo(cookie);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
        }
    }
}
