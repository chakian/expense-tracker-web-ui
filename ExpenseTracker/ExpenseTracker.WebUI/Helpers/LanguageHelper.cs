using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace ExpenseTracker.WebUI.Helpers
{
    public class Language
    {
        public Language(LanguageName name, LanguageCode code, string defaultLongCode)
        {
            Name = name;
            Code = code;
            DefaultLongCode = defaultLongCode;
        }

        public LanguageName Name { get; private set; }
        public LanguageCode Code { get; private set; }
        public string DefaultLongCode { get; private set; }

        public enum LanguageName
        {
            Turkish,
            English
        }

        public enum LanguageCode
        {
            tr,
            en
        }
    }

    public class LanguageHelper
    {
        private const string DEFAULT_CULTURE = "tr-TR";

        public static List<Language> AvailableLanguages = new List<Language>()
        {
            new Language(Language.LanguageName.Turkish, Language.LanguageCode.tr, "tr-TR"),
            new Language(Language.LanguageName.English, Language.LanguageCode.en, "en-GB")
        };

        public static void SetLanguage(string cultureCode)
        {
            string cultureString;

            if (!string.IsNullOrEmpty(cultureCode) && AvailableLanguages.Any(q => cultureCode.StartsWith(q.Code.ToString())))
            {
                var foundLanguage = AvailableLanguages.SingleOrDefault(q => cultureCode.Equals(q.Code.ToString(), System.StringComparison.InvariantCultureIgnoreCase));

                if (foundLanguage != null)
                {
                    cultureString = foundLanguage.DefaultLongCode;
                }
                else
                {
                    cultureString = cultureCode;
                }
            }
            else
            {
                cultureString = DEFAULT_CULTURE;
            }

            CultureInfo cultureInfo = new CultureInfo(cultureString);

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
    }
}