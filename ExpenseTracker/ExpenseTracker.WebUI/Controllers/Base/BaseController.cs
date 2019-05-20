using ExpenseTracker.Business;
using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Context.DbModels;
using ExpenseTracker.WebUI.Helpers;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
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

        protected int ActiveBudgetId
        {
            get
            {
                var id = (int?)ViewBag.ActiveBudgetId;
                return id ?? -1;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            setActiveBudgetProperties();
        }

        private void setActiveBudgetProperties()
        {
            Budget budget = null;

            int? activeBudgetId = (int?)Session["ActiveBudgetId"];
            if (!activeBudgetId.HasValue)
            {
                budget = getBudgetFromDb();
                if (budget != null)
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

            string activeBudgetName = (string)Session["ActiveBudgetName"];
            if (string.IsNullOrEmpty(activeBudgetName) && budget != null)
            {
                activeBudgetName = budget.Name;
                Session["ActiveBudgetName"] = activeBudgetName;
            }
            ViewBag.ActiveBudgetName = activeBudgetName;
        }

        private Budget getBudgetFromDb()
        {
            Budget budget = getActiveButgetFromUserPreferences();

            if (budget == null)
            {
                budget = getUsersFirstBudget();
            }

            return budget;
        }

        private Budget getActiveButgetFromUserPreferences()
        {
            var user = context.Users.Find(UserId);
            if(user != null && user.ActiveBudgetId.HasValue)
            {
                return new BudgetBusiness(context).GetBudgetDetails(user.ActiveBudgetId.Value, UserId);
            }
            return null;
        }

        private Budget getUsersFirstBudget()
        {
            Budget budget = new BudgetBusiness(context).GetBudgetsOfUser(UserId).FirstOrDefault();
            if (budget != null)
            {
                var user = context.Users.Find(UserId);
                user.ActiveBudgetId = budget.BudgetId;
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
            }
            return budget;
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
