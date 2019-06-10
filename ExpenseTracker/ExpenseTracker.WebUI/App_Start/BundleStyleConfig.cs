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

            bundles.Add(new StyleBundle("~/Content/theme/startbootstrap").Include(
                      "~/Content/startbs/simple-sidebar.css"
                      ));
        }
    }
}