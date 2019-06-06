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

            bundles.Add(new StyleBundle("~/Content/theme/prodadmcss").Include(
                      "~/Content/theme/prod_adm/css/fontawesome.min.css",
                      "~/Content/theme/prod_adm/css/templatemo-style.css"
                      ));

            bundles.Add(new StyleBundle("~/bundles/datepickercss").Include(
                      "~/Content/theme/prod_adm/jquery-ui-datepicker/jquery-ui.min.css"
                      ));
        }
    }
}