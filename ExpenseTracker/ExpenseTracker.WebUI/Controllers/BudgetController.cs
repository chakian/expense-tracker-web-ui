using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ExpenseTracker.Persistence.Context.DbModels;
using Microsoft.AspNet.Identity;

namespace ExpenseTracker.WebUI.Controllers
{
    [Authorize]
    public class BudgetController : BaseController
    {
        private const string UNAUTHORIZED_MESSAGE = "Bu bütçe üzerinde yetkiniz bulunmamaktadır";
        // GET: Budget
        public ActionResult Index()
        {
            var budgets = db.Budgets.Where(b => b.IsActive && b.BudgetUsers.Any(bu => bu.UserId.Equals(UserId))).Include(b => b.Currency);//.Include(b => b.InsertUser).Include(b => b.UpdateUser);
            return View(budgets.ToList());
        }

        // GET: Budget/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Budget budget = db.Budgets.Find(id);

            if (budget == null)
            {
                return HttpNotFound();
            }
            else if (!budget.BudgetUsers.Any(bu => bu.UserId.Equals(UserId)))
            {
                return ReturnUnauthorized(UNAUTHORIZED_MESSAGE);
            }

            budget.InsertUser = db.Users.Find(budget.InsertUserId);
            budget.UpdateUser = db.Users.Find(budget.UpdateUserId);

            return View(budget);
        }

        // GET: Budget/Create
        public ActionResult Create()
        {
            ViewBag.CurrencyId = new SelectList(db.Currencies, "CurrencyId", "CurrencyCode");
            return View();
        }

        // POST: Budget/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BudgetId,Name,CurrencyId")] Budget budget)
        {
            if (ModelState.IsValid)
            {
                budget.InsertUserId = UserId;
                budget.InsertTime = DateTime.Now;
                budget.UpdateUserId = UserId;
                budget.UpdateTime = DateTime.Now;
                budget.IsActive = true;

                db.Budgets.Add(budget);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CurrencyId = new SelectList(db.Currencies, "CurrencyId", "CurrencyCode", budget.CurrencyId);
            return View(budget);
        }

        // GET: Budget/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Budget budget = db.Budgets.Find(id);

            if (budget == null)
            {
                return HttpNotFound();
            }
            else if (!budget.BudgetUsers.Any(bu => bu.UserId.Equals(UserId)))
            {
                return ReturnUnauthorized(UNAUTHORIZED_MESSAGE);
            }
            ViewBag.CurrencyId = new SelectList(db.Currencies, "CurrencyId", "CurrencyCode", budget.CurrencyId);
            return View(budget);
        }

        // POST: Budget/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BudgetId,Name,CurrencyId,InsertUserId,InsertTime,IsActive")] Budget budget)
        {
            if (ModelState.IsValid)
            {
                var budgetUserList = db.BudgetUsers.AsNoTracking().Where(bu => bu.BudgetId.Equals(budget.BudgetId)).ToList();
                if (!budgetUserList.Any(bu => bu.UserId.Equals(UserId)))
                {
                    return ReturnUnauthorized(UNAUTHORIZED_MESSAGE);
                }

                budget.UpdateUserId = UserId;
                budget.UpdateTime = DateTime.Now;

                db.Entry(budget).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CurrencyId = new SelectList(db.Currencies, "CurrencyId", "CurrencyCode", budget.CurrencyId);
            return View(budget);
        }

        // GET: Budget/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Budget budget = db.Budgets.Find(id);
            if (budget == null)
            {
                return HttpNotFound();
            }
            else if (!budget.BudgetUsers.Any(bu => bu.UserId.Equals(UserId)))
            {
                return ReturnUnauthorized(UNAUTHORIZED_MESSAGE);
            }
            return View(budget);
        }

        // POST: Budget/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Budget budget = db.Budgets.Find(id);

            var budgetUserList = db.BudgetUsers.AsNoTracking().Where(bu => bu.BudgetId.Equals(budget.BudgetId)).ToList();
            if (!budgetUserList.Any(bu => bu.UserId.Equals(UserId)))
            {
                return ReturnUnauthorized(UNAUTHORIZED_MESSAGE);
            }

            db.Budgets.Remove(budget);
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
