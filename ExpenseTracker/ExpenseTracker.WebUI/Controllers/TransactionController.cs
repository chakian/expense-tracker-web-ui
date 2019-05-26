using ExpenseTracker.Business;
using ExpenseTracker.WebUI.Models.Transaction;
using System.Linq;
using System.Web.Mvc;

namespace ExpenseTracker.WebUI.Controllers
{
    public class TransactionController : BaseController
    {
        readonly BudgetAccountBusiness budgetAccountBusiness;
        readonly CategoryBusiness categoryBusiness;
        readonly TransactionBusiness transactionBusiness;

        public TransactionController()
        {
            budgetAccountBusiness = new BudgetAccountBusiness(context);
            categoryBusiness = new CategoryBusiness(context);
            transactionBusiness = new TransactionBusiness(context);
        }

        [HttpGet]
        public ActionResult Create()
        {
            AddModel model = new AddModel();

            SetAccountListForModel(model);
            SetCategoryListForModel(model);

            return View(model);
        }

        private void SetCategoryListForModel(AddModel model)
        {
            var categories = categoryBusiness.GetCategoriesByBudgetId(ActiveBudgetId, UserId);
            if (categories != null)
            {
                model.CategoryList = new SelectList(categories.OrderBy(q => q.Name), "CategoryId", "Name");
            }
        }

        private void SetAccountListForModel(AddModel model)
        {
            var accounts = budgetAccountBusiness.GetAccountsOfUser(UserId, ActiveBudgetId);
            if (accounts != null)
            {
                model.AccountList = new SelectList(accounts, "AccountId", "Name");
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(AddModel model)
        {
            if (ModelState.IsValid)
            {
                decimal amount = (model.IsIncome == true ? 1 : -1) * model.Amount;
                transactionBusiness.InsertTransaction(UserId, model.CategoryId, model.AccountId, amount, model.Description, model.Date);
                return RedirectToAction("Create");
            }

            SetAccountListForModel(model);
            SetCategoryListForModel(model);
            
            return View(model);
        }
    }
}
