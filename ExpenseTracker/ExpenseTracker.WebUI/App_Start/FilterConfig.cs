using ExpenseTracker.WebUI.Helpers;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTracker.WebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            //filters.Add(new ErrorHandler.AiHandleErrorAttribute());
            //filters.Add(new HandleErrorAttribute());
            //filters.Add(new LocalizationAttribute(), 0);
        }
    }
}
