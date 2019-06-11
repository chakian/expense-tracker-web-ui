using ExpenseTracker.Business;
using ExpenseTracker.WebUI.Models;
using ExpenseTracker.WebUI.Models.Transaction;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ExpenseTracker.WebUI.Controllers
{
    public class TransactionController : BaseAuthenticatedController
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

            model.Date = DateTime.Now;

            return View(model);
        }

        private void SetCategoryListForModel(BaseEditableTransactionModel model)
        {
            var categories = categoryBusiness.GetCategoriesByBudgetId(ActiveBudgetId, UserId);
            if (categories != null)
            {
                model.CategoryList = new SelectList(categories.OrderBy(q => q.Name), "CategoryId", "Name");
            }
        }

        private void SetAccountListForModel(BaseEditableTransactionModel model)
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

        [HttpGet]
        public ActionResult Edit(int TransactionId)
        {
            EditModel model = new EditModel();

            model.TransactionId = TransactionId;

            var transaction = transactionBusiness.GetTransactionById(TransactionId, UserId);
            if(transaction.Amount < 0)
            {
                model.Amount = transaction.Amount * -1;
                model.IsIncome = false;
            }
            else
            {
                model.Amount = transaction.Amount;
                model.IsIncome = true;
            }
            model.Date = transaction.Date;
            model.Description = transaction.Description;
            model.CategoryId = transaction.CategoryId;
            model.AccountId = transaction.SourceAccountId;

            SetAccountListForModel(model);
            SetCategoryListForModel(model);
            SetTransactionSummaryListForModel(model);

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(EditModel model)
        {
            if (ModelState.IsValid)
            {
                decimal amount = (model.IsIncome == true ? 1 : -1) * model.Amount;
                transactionBusiness.UpdateTransaction(model.TransactionId, UserId, model.CategoryId, model.AccountId, amount, model.Description, model.Date);
                return RedirectToAction("List");
            }

            var transaction = transactionBusiness.GetTransactionById(model.TransactionId, UserId);
            if (transaction.Amount < 0)
            {
                model.Amount *= -1;
                model.IsIncome = false;
            }
            else
            {
                model.IsIncome = true;
            }

            SetAccountListForModel(model);
            SetCategoryListForModel(model);
            SetTransactionSummaryListForModel(model);

            return View(model);
        }

        [HttpGet]
        public ActionResult List()
        {
            ListModel model = new ListModel();

            SetTransactionSummaryListForModel(model);

            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int TransactionId)
        {
            DeleteModel model = new DeleteModel();

            model.TransactionId = TransactionId;

            var transaction = transactionBusiness.GetTransactionById(TransactionId, UserId);

            model.Amount = transaction.Amount;
            model.Date = transaction.Date;
            model.Description = transaction.Description;
            model.CategoryName = transaction.Category.Name;
            model.AccountName = transaction.SourceAccount.Name;

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Delete(DeleteModel model)
        {
            if (ModelState.IsValid)
            {
                transactionBusiness.DeleteTransaction(model.TransactionId, UserId);
                return RedirectToAction("List");
            }

            model.TransactionId = model.TransactionId;

            var transaction = transactionBusiness.GetTransactionById(model.TransactionId, UserId);

            model.Amount = transaction.Amount;
            model.Date = transaction.Date;
            model.Description = transaction.Description;
            model.CategoryName = transaction.Category.Name;
            model.AccountName = transaction.SourceAccount.Name;

            
            return View(model);
        }
    }
}
