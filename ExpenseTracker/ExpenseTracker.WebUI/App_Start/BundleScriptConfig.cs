using System.Web.Optimization;

namespace ExpenseTracker.WebUI
{
    public class BundleScriptConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"
                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/theme/prodadmjs").Include(
                      "~/Content/theme/prod_adm/js/Chart.min.js",
                      "~/Content/theme/prod_adm/js/moment.min.js",
                      "~/Content/theme/prod_adm/js/tooplate-scripts.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/datepickerjs").Include(
                      "~/Content/theme/prod_adm/jquery-ui-datepicker/jquery-ui.min.js"
                      ));
        }
    }
}