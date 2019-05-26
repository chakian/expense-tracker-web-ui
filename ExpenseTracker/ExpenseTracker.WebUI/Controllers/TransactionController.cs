using ExpenseTracker.Business;
using ExpenseTracker.WebUI.Models;
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
        public ActionResult Add()
        {
            AddModel model = new AddModel();

            SetAccountListForModel(model);
            SetCategoryListForModel(model);
            SetTransactionSummaryListForModel(model);

            return View(model);
        }

        private void SetCategoryListForModel(BaseTransactionModel model)
        {
            var categories = categoryBusiness.GetCategoriesByBudgetId(ActiveBudgetId, UserId);
            if (categories != null)
            {
                model.CategoryList = new SelectList(categories.OrderBy(q => q.Name), "CategoryId", "Name");
            }
        }

        private void SetAccountListForModel(BaseTransactionModel model)
        {
            var accounts = budgetAccountBusiness.GetAccountsOfUser(UserId, ActiveBudgetId);
            if (accounts != null)
            {
                model.AccountList = new SelectList(accounts, "AccountId", "Name");
            }
        }

        private void SetTransactionSummaryListForModel(BaseTransactionModel model)
        {
            model.TransactionSummaries = new System.Collections.Generic.List<BaseTransactionModel.TransactionSummary>();

            var transactions = transactionBusiness.GetTransactionsForCurrentPeriod(UserId, ActiveBudgetId).OrderByDescending(q => q.Date).ToList();
            if (transactions != null)
            {
                transactions.ForEach(t =>
                {
                    model.TransactionSummaries.Add(new BaseTransactionModel.TransactionSummary
                    {
                        TransactionId = t.TransactionId,
                        Date = t.Date,
                        Amount = t.Amount,
                        Account = t.SourceAccount.Name,
                        Category = t.Category.Name,
                        Description = t.Description
                    });
                });
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Add(AddModel model)
        {
            if (ModelState.IsValid)
            {
                decimal amount = (model.IsIncome == true ? 1 : -1) * model.Amount;
                transactionBusiness.InsertTransaction(UserId, model.CategoryId, model.AccountId, amount, model.Description, model.Date);
                return RedirectToAction("Add");
            }

            SetAccountListForModel(model);
            SetCategoryListForModel(model);
            SetTransactionSummaryListForModel(model);

            return View(model);
        }
    }
}
