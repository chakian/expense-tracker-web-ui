using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ExpenseTracker.Business;
using ExpenseTracker.Persistence.Context.DbModels;

namespace ExpenseTracker.WebUI.Controllers
{
    [Authorize]
    public class BudgetController : BaseController
    {
        //TODO: private const string UNAUTHORIZED_MESSAGE = "Bu bütçe üzerinde yetkiniz bulunmamaktadır";

        private readonly BudgetBusiness budgetBusiness;

        public BudgetController()
        {
            budgetBusiness = new BudgetBusiness(context);
        }

        // GET: Budget
        public ActionResult Index()
        {
            List<Budget> budgets = budgetBusiness.GetBudgetsOfUser(UserId);
            return View(budgets.ToList());
        }

        // GET: Budget/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Budget budget = budgetBusiness.GetBudgetDetails(id.Value, UserId);

            if (budget == null)
            {
                //TODO: return ReturnUnauthorized(UNAUTHORIZED_MESSAGE);
                return HttpNotFound();
            }

            return View(budget);
        }

        // GET: Budget/Create
        public ActionResult Create()
        {
            ViewBag.CurrencyId = new SelectList(context.Currencies, "CurrencyId", "CurrencyCode");
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

                context.Budgets.Add(budget);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CurrencyId = new SelectList(context.Currencies, "CurrencyId", "CurrencyCode", budget.CurrencyId);
            return View(budget);
        }

        // GET: Budget/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Budget budget = context.Budgets.Find(id);

            if (budget == null)
            {
                return HttpNotFound();
            }
            else if (!budget.BudgetUsers.Any(bu => bu.UserId.Equals(UserId)))
            {
                return ReturnUnauthorized(UNAUTHORIZED_MESSAGE);
            }
            ViewBag.CurrencyId = new SelectList(context.Currencies, "CurrencyId", "CurrencyCode", budget.CurrencyId);
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
                var budgetUserList = context.BudgetUsers.AsNoTracking().Where(bu => bu.BudgetId.Equals(budget.BudgetId)).ToList();
                if (!budgetUserList.Any(bu => bu.UserId.Equals(UserId)))
                {
                    return ReturnUnauthorized(UNAUTHORIZED_MESSAGE);
                }

                budget.UpdateUserId = UserId;
                budget.UpdateTime = DateTime.Now;

                context.Entry(budget).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CurrencyId = new SelectList(context.Currencies, "CurrencyId", "CurrencyCode", budget.CurrencyId);
            return View(budget);
        }

        // GET: Budget/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Budget budget = context.Budgets.Find(id);
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
            Budget budget = context.Budgets.Find(id);

            var budgetUserList = context.BudgetUsers.AsNoTracking().Where(bu => bu.BudgetId.Equals(budget.BudgetId)).ToList();
            if (!budgetUserList.Any(bu => bu.UserId.Equals(UserId)))
            {
                return ReturnUnauthorized(UNAUTHORIZED_MESSAGE);
            }

            context.Budgets.Remove(budget);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
