using System.Web.Mvc;

namespace ExpenseTracker.WebUI.Helpers
{
    public class LocalizationAttribute : ActionFilterAttribute
    {
        private string _DefaultLanguage = AvailableLanguages.TR;

        public LocalizationAttribute(string defaultLanguage)
        {
            _DefaultLanguage = defaultLanguage;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string lang = (string)filterContext.RouteData.Values["lang"] ?? _DefaultLanguage;
            LanguageHelper.SetLanguage(lang);
        }
    }
}