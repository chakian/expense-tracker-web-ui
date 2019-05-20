using ExpenseTracker.Business;
using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Context.DbModels;
using ExpenseTracker.WebUI.Helpers;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace ExpenseTracker.WebUI.Controllers
{
    [RequireHttps]
    public class BaseController : Controller
    {
        protected readonly ExpenseTrackerContext context;

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

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            int? activeBudgetId = (int?)Session["ActiveBudgetId"];
            if (!activeBudgetId.HasValue)
            {
                Budget budget = new BudgetBusiness(context).GetBudgetsOfUser(UserId).FirstOrDefault();
                if(budget != null)
                {
                    activeBudgetId = budget.BudgetId;
                    Session["ActiveBudgetId"] = activeBudgetId.Value;
                }
                else
                {
                    activeBudgetId = -1;
                }
            }
            ViewBag.ActiveBudgetId = activeBudgetId.Value;
        }

        public BaseController()
        {
            context = DbContextFactory.GetExpenseTrackerContext();
        }

        protected ActionResult ReturnUnauthorized(string message = null)
        {
            return new HttpUnauthorizedResult(string.IsNullOrEmpty(message) ? "Yetkisiz İşlem" : message);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
