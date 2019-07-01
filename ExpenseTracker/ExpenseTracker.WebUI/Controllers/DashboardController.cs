using System.Web.Mvc;

namespace ExpenseTracker.WebUI.Controllers
{
    public class DashboardController : BaseAuthenticatedController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}