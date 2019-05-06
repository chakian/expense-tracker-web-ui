using ExpenseTracker.WebUI.Models.User;
using System.Web.Mvc;

namespace ExpenseTracker.WebUI.Controllers
{
    public class UserController : BaseController
    {
        public ActionResult Login()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            //TODO: Perform login
            SetAuthenticationValues(model);
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Logout()
        {
            //TODO: Perform logout
            return RedirectToAction("Index", "Home");
        }
    }
}