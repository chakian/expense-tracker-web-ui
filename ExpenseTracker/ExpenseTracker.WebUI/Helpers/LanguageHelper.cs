using System.Globalization;
using System.Threading;

namespace ExpenseTracker.WebUI.Helpers
{
    public class AvailableLanguages
    {
        public const string TR = "tr-TR";
        public const string TR_Short = "tr";
        public const string EN = "en-GB";
        public const string EN_Short = "en";
    }

    public class LanguageHelper
    {
        //fixed number and date format for now, this can be improved.
        public static void SetLanguage(string language)
        {
            string cultureString;

            switch (language)
            {
                case AvailableLanguages.EN:
                case AvailableLanguages.EN_Short:
                    cultureString = "en-GB";
                    break;
                case AvailableLanguages.TR:
                case AvailableLanguages.TR_Short:
                default:
                    cultureString = "tr-TR";
                    break;
            }

            CultureInfo cultureInfo = new CultureInfo(cultureString);

            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = cultureInfo;
        }
    }
}