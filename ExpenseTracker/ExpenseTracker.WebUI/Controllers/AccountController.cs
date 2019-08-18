using System;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using ExpenseTracker.Business;
using ExpenseTracker.Entities;
using ExpenseTracker.WebUI.Models.Account;

namespace ExpenseTracker.WebUI.Controllers
{
    public class AccountController : BaseAuthenticatedController
    {
        private readonly AccountBusiness accountBusiness;

        public AccountController()
        {
            accountBusiness = new AccountBusiness();
        }

        public ActionResult Index()
        {
            var accounts = accountBusiness.GetAccountsOfUser(UserId, ActiveBudgetId);
            return View(accounts);
        }

        public ActionResult Create()
        {
            SetViewBagValues(null);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateAccountModel model)
        {
            if (ModelState.IsValid && new BudgetBusiness().GetBudgetDetails(ActiveBudgetId, UserId) != null)
            {
                AccountEntity account = new AccountEntity
                {
                    Name = model.Name,
                    StartingBalance = model.StartingBalance,
                    CurrentBalance = model.StartingBalance,
                    BudgetId = ActiveBudgetId,
                    AccountTypeId = model.AccountTypeId
                };

                accountBusiness.CreateAccount(account, UserId);
                return RedirectToAction("Index");
            }

            SetViewBagValues(model.AccountTypeId);
            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AccountEntity account = accountBusiness.GetAccountById(id.Value, UserId);

            if (account == null)
            {
                return HttpNotFound();
            }

            SetViewBagValues(account.AccountTypeId);
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateAccountModel model)
        {
            if (ModelState.IsValid)
            {
                AccountEntity accountEntity = new AccountEntity
                {
                    Name = model.Name,
                    CurrentBalance = model.StartingBalance,
                };

                accountBusiness.UpdateAccount(accountEntity, UserId);
                return RedirectToAction("Index");
            }

            SetViewBagValues(model.AccountTypeId);
            return View(model);
        }

        private void SetViewBagValues(int? selectedAccountTypeId)//, int? selectedBudgetId)
        {
            AccountTypeBusiness accountTypeBusiness = new AccountTypeBusiness();

            ViewBag.AccountTypeId = new SelectList(accountTypeBusiness.GetAccountTypeList(), "AccountTypeId", "Name", selectedAccountTypeId);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int AccountId)
        {
            accountBusiness.DeleteAccount(AccountId, UserId);

            return RedirectToAction("Index");
        }
    }
}
