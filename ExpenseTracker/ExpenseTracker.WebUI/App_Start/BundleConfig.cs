using System.Web.Optimization;

namespace ExpenseTracker.WebUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleScriptConfig.RegisterBundles(bundles);

            BundleStyleConfig.RegisterBundles(bundles);
        }
    }
}
