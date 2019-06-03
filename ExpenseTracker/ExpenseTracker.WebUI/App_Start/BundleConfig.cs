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

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/theme/chimpcss").Include(
                      "~/Content/theme/chimp/css/aos.css",
                      "~/Content/theme/chimp/css/bootstrap.min.css",
                      "~/Content/theme/chimp/css/bootstrap-datepicker.css",
                      "~/Content/theme/chimp/css/jquery-ui.css",
                      "~/Content/theme/chimp/css/magnific-popup.css",
                      "~/Content/theme/chimp/css/mediaelementplayer.css",
                      "~/Content/theme/chimp/css/owl.carousel.min.css",
                      "~/Content/theme/chimp/css/owl.theme.default.min.css",
                      "~/Content/theme/chimp/css/style.css",
                      "~/Content/theme/chimp/css/bootstrap/bootstrap.css",
                      "~/Content/theme/chimp/css/bootstrap/bootstrap-grid.css",
                      "~/Content/theme/chimp/css/bootstrap/bootstrap-reboot.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/theme/chimpjs").Include(
                      "~/Content/theme/chimp/js/aos.js",
                      "~/Content/theme/chimp/js/bootstrap.min.js",
                      "~/Content/theme/chimp/js/bootstrap-datepicker.min.js",
                      "~/Content/theme/chimp/js/jquery.countdown.min.js",
                      "~/Content/theme/chimp/js/jquery.magnific-popup.min.js",
                      "~/Content/theme/chimp/js/jquery.stellar.min.js",
                      "~/Content/theme/chimp/js/jquery-3.3.1.min.js",
                      "~/Content/theme/chimp/js/jquery-migrate-3.0.1.min.js",
                      "~/Content/theme/chimp/js/jquery-ui.js",
                      "~/Content/theme/chimp/js/main.js",
                      "~/Content/theme/chimp/js/mediaelement-and-player.min.js",
                      "~/Content/theme/chimp/js/owl.carousel.min.js",
                      "~/Content/theme/chimp/js/popper.min.js",
                      "~/Content/theme/chimp/js/slick.min.js",
                      "~/Content/theme/chimp/js/typed.js"
                      ));
        }
    }
}
