using ExpenseTracker.Business;
using ExpenseTracker.Entities;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace ExpenseTracker.WebUI.Controllers
{
    [RequireHttps]
    public class BaseController : Controller
    {
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

            SetActiveBudgetProperties();
        }

        private void SetActiveBudgetProperties()
        {
            BudgetEntity budget = null;

            int? activeBudgetId = (int?)Session["ActiveBudgetId"];
            if (!activeBudgetId.HasValue)
            {
                budget = GetActiveBudget();
                if (budget != null)
                {
                    activeBudgetId = budget.BudgetId;
                    Session["ActiveBudgetId"] = activeBudgetId.Value;
                }
                else
                {
                    /// TODO: We should create a budget and set is as the "active budget" in any case. 
                    /// Therefore if this block is hit during execution, there has to be a problem somewhere.
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

        private BudgetEntity GetActiveBudget()
        {
            BudgetEntity budget = GetActiveButgetFromUserPreferences();

            if (budget == null)
            {
                budget = GetUsersFirstBudget();
            }

            return budget;
        }

        private BudgetEntity GetActiveButgetFromUserPreferences()
        {
            var user = new UserBusiness().GetUserById(UserId);
            if (user != null && user.ActiveBudgetId.HasValue)
            {
                return new BudgetBusiness().GetBudgetDetails(user.ActiveBudgetId.Value, UserId);
            }
            return null;
        }

        private BudgetEntity GetUsersFirstBudget()
        {
            BudgetEntity budget = new BudgetBusiness().GetUsersFirstBudgetAndSetAsDefault(UserId);
            
            return budget;
        }

        protected ActionResult ReturnUnauthorized(string message = null)
        {
            return new HttpUnauthorizedResult(string.IsNullOrEmpty(message) ? "Yetkisiz İşlem" : message);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
    }
}
