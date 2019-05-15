using ExpenseTracker.Persistence.Context;
using ExpenseTracker.WebUI.Helpers;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace ExpenseTracker.WebUI.Controllers
{
    [RequireHttps]
    public class BaseController : Controller
    {
        protected readonly ExpenseTrackerContext db;

        protected string UserId
        {
            get
            {
                if(User != null && User.Identity != null)
                {
                    return User.Identity.GetUserId();
                }
                return string.Empty;
            }
        }

        public BaseController()
        {
            db = DbContextFactory.GetExpenseTrackerContext();
        }

        protected ActionResult ReturnUnauthorized(string message = null)
        {
            return new HttpUnauthorizedResult(string.IsNullOrEmpty(message) ? "Yetkisiz İşlem" : message);
        }
    }
}
