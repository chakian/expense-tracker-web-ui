using ExpenseTracker.WebUI.Models;
using System.Web.Mvc;

namespace ExpenseTracker.WebUI.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                //return RedirectToAction("Index", "Dashboard");
                return RedirectToAction("Add", "Transaction");
            }
            else
            {
                BaseModel model = new BaseModel();
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Contact()
        {
            BaseModel model = new BaseModel();
            return View(model);
        }
    }
}
