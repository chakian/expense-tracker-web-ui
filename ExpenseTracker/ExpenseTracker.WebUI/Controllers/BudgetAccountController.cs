using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ExpenseTracker.Business.Context;
using ExpenseTracker.Business.Context.DbModels;
using Microsoft.AspNet.Identity;

namespace ExpenseTracker.WebUI.Controllers
{
    [Authorize]
    public class BudgetAccountController : Controller
    {
        private ExpenseTrackerContext db = new ExpenseTrackerContext();

        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();

            var accounts = db.Accounts.Where(a => a.Budget.BudgetUsers.Any(bu => bu.UserId.Equals(userId))).Include(a => a.AccountType).Include(a => a.Budget);
            return View(accounts.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        public ActionResult Create()
        {
            string userId = User.Identity.GetUserId();

            ViewBag.AccountTypeId = new SelectList(db.AccountTypes, "AccountTypeId", "Name");
            ViewBag.BudgetId = new SelectList(db.Budgets.Where(b => b.BudgetUsers.Any(bu => bu.UserId.Equals(userId))), "BudgetId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountId,Name,StartingBalance,CurrentBalance,AccountTypeId,BudgetId")] Account account)
        {
            if (ModelState.IsValid)
            {
                account.InsertUserId = User.Identity.GetUserId();
                account.InsertTime = DateTime.Now;
                account.UpdateUserId = User.Identity.GetUserId();
                account.UpdateTime = DateTime.Now;
                account.IsActive = true;

                account.CurrentBalance = account.StartingBalance;

                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountTypeId = new SelectList(db.AccountTypes, "AccountTypeId", "Name", account.AccountTypeId);
            ViewBag.BudgetId = new SelectList(db.Budgets, "BudgetId", "Name", account.BudgetId);
            return View(account);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Include(a => a.InsertUser).Include(a => a.UpdateUser).FirstOrDefault(a => a.AccountId.Equals(id.Value));
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountTypeId = new SelectList(db.AccountTypes, "AccountTypeId", "Name", account.AccountTypeId);
            ViewBag.BudgetId = new SelectList(db.Budgets, "BudgetId", "Name", account.BudgetId);
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountId,Name,StartingBalance,CurrentBalance,AccountTypeId,BudgetId,InsertUserId,InsertTime,IsActive")] Account account)
        {
            if (ModelState.IsValid)
            {
                account.UpdateUserId = User.Identity.GetUserId();
                account.UpdateTime = DateTime.Now;

                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountTypeId = new SelectList(db.AccountTypes, "AccountTypeId", "Name", account.AccountTypeId);
            ViewBag.BudgetId = new SelectList(db.Budgets, "BudgetId", "Name", account.BudgetId);
            return View(account);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
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
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
