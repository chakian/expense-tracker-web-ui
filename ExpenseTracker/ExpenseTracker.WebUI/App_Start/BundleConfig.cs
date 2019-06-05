using System.Web;
using System.Web.Optimization;

namespace ExpenseTracker.WebUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
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

            bundles.Add(new StyleBundle("~/Content/bootstrapcss").Include(
                      "~/Content/bootstrap.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/theme/prodadmcss").Include(
                      "~/Content/theme/prod_adm/css/fontawesome.min.css",
                      "~/Content/theme/prod_adm/css/templatemo-style.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/theme/prodadmjs").Include(
                      "~/Content/theme/chimp/js/bootstrap.min.js",
                      "~/Content/theme/chimp/js/Chart.min.js",
                      "~/Content/theme/chimp/js/moment.min.js",
                      "~/Content/theme/chimp/js/tooplate-scripts.js"
                      ));
        }
    }
}
