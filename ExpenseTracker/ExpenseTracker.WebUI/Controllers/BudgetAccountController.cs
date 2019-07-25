using System;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using ExpenseTracker.Business;
using ExpenseTracker.Persistence.Context.DbModels;

namespace ExpenseTracker.WebUI.Controllers
{
    public class BudgetAccountController : BaseAuthenticatedController
    {
        private readonly BudgetAccountBusiness budgetAccountBusiness;

        public BudgetAccountController()
        {
            budgetAccountBusiness = new BudgetAccountBusiness(context);
        }

        public ActionResult Index()
        {
            var accounts = budgetAccountBusiness.GetAccountsOfUser(UserId, ActiveBudgetId);
            return View(accounts);
        }

        public ActionResult Create()
        {
            SetViewBagValues(null);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountId,Name,StartingBalance,CurrentBalance,AccountTypeId")] Account account)
        {
            if (ModelState.IsValid && new BudgetBusiness(context).GetBudgetDetails(ActiveBudgetId, UserId) != null)
            {
                account.BudgetId = ActiveBudgetId;

                account.InsertUserId = UserId;
                account.InsertTime = DateTime.Now;
                account.UpdateUserId = UserId;
                account.UpdateTime = DateTime.Now;
                account.IsActive = true;

                account.CurrentBalance = account.StartingBalance;

                //TODO: Do not use context in Web project. Use the business methods instead!
                context.Accounts.Add(account);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            SetViewBagValues(account.AccountTypeId);
            return View(account);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Account account = budgetAccountBusiness.GetAccountById(id.Value, UserId);
            
            if (account == null)
            {
                return HttpNotFound();
            }

            SetViewBagValues(account.AccountTypeId);
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountId,Name,StartingBalance,CurrentBalance,AccountTypeId,BudgetId,InsertUserId,InsertTime,IsActive")] Account account)
        {
            if (ModelState.IsValid)
            {
                account.UpdateUserId = UserId;
                account.UpdateTime = DateTime.Now;

                //TODO: Do not use context in Web project. Use the business methods instead!
                context.Entry(account).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            SetViewBagValues(account.AccountTypeId);
            return View(account);
        }

        private void SetViewBagValues(int? selectedAccountTypeId)//, int? selectedBudgetId)
        {
            AccountTypeBusiness accountTypeBusiness = new AccountTypeBusiness(context);

            ViewBag.AccountTypeId = new SelectList(accountTypeBusiness.GetAccountTypeList(), "AccountTypeId", "Name", selectedAccountTypeId);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int AccountId)
        {
            Account account = budgetAccountBusiness.GetAccountById(AccountId, UserId);

            if(account != null)
            {
                account.UpdateUserId = UserId;
                account.UpdateTime = DateTime.Now;
                account.IsActive = false;

                //TODO: Do not use context in Web project. Use the business methods instead!
                context.Entry(account).State = EntityState.Modified;
                context.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }
    }
}
