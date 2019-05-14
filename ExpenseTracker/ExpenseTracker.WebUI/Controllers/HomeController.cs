using ExpenseTracker.WebUI.Models;
using System.Web.Mvc;

namespace ExpenseTracker.WebUI.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            BaseModel model = new BaseModel();
            return View(model);
        }
    }
}
