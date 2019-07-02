using System.Web.Optimization;

namespace ExpenseTracker.WebUI
{
    public class BundleStyleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/bootstrapcss").Include(
                      "~/Content/bootstrap.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/adminltetemplate").Include(
                      "~/Content/adminlte/dist/css/adminlte.css",
                      "~/Content/adminlte/plugins/fontawesome-free/css/all.css"
                      ));
        }
    }
}