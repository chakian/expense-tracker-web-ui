using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ExpenseTracker.WebUI.Startup))]
namespace ExpenseTracker.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}