using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ExpenseTracker.Business;
using ExpenseTracker.Persistence.Context.DbModels;

namespace ExpenseTracker.WebUI.Controllers
{
    [Authorize]
    public class BudgetAccountController : BaseController
    {
        private readonly BudgetAccountBusiness budgetAccountBusiness;

        public BudgetAccountController()
        {
            budgetAccountBusiness = new BudgetAccountBusiness(context);
        }

        public ActionResult Index()
        {
            var accounts = budgetAccountBusiness.GetAccountsOfUser(UserId);
            return View(accounts);
        }

        public ActionResult Details(int? id)
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
            return View(account);
        }

        public ActionResult Create()
        {
            SetViewBagValues();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountId,Name,StartingBalance,CurrentBalance,AccountTypeId,BudgetId")] Account account)
        {
            if (ModelState.IsValid && new BudgetBusiness(context).GetBudgetDetails(account.BudgetId, UserId) != null)
            {
                account.InsertUserId = UserId;
                account.InsertTime = DateTime.Now;
                account.UpdateUserId = UserId;
                account.UpdateTime = DateTime.Now;
                account.IsActive = true;

                account.CurrentBalance = account.StartingBalance;

                context.Accounts.Add(account);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            SetViewBagValues();
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

            SetViewBagValues();
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

                context.Entry(account).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            SetViewBagValues();
            return View(account);
        }

        private void SetViewBagValues()
        {
            BudgetBusiness budgetBusiness = new BudgetBusiness(context);
            AccountTypeBusiness accountTypeBusiness = new AccountTypeBusiness(context);

            ViewBag.AccountTypeId = new SelectList(accountTypeBusiness.GetAccountTypeList(), "AccountTypeId", "Name");
            ViewBag.BudgetId = new SelectList(budgetBusiness.GetBudgetsOfUser(UserId), "BudgetId", "Name");
        }

        public ActionResult Delete(int? id)
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
            return View(account);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = budgetAccountBusiness.GetAccountById(id, UserId);

            if(account != null)
            {
                context.Accounts.Remove(account);
                context.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }
    }
}
