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
        private const string UNAUTHORIZED_MESSAGE = "Bu bütçe üzerinde yetkiniz bulunmamaktadır";

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
            CurrencyBusiness currencyBusiness = new CurrencyBusiness(context);
            ViewBag.CurrencyId = new SelectList(currencyBusiness.GetCurrencyList(), "CurrencyId", "CurrencyCode");

            return View();
        }

        // POST: Budget/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BudgetId,Name,CurrencyId")] Budget budget)
        {
            if (ModelState.IsValid)
            {
                budgetBusiness.CreateBudget(budget.Name, budget.CurrencyId, UserId);
                return RedirectToAction("Index");
            }

            CurrencyBusiness currencyBusiness = new CurrencyBusiness(context);
            ViewBag.CurrencyId = new SelectList(currencyBusiness.GetCurrencyList(), "CurrencyId", "CurrencyCode", budget.CurrencyId);
            return View(budget);
        }

        // GET: Budget/Edit/5
        public ActionResult Edit(int? id)
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

            CurrencyBusiness currencyBusiness = new CurrencyBusiness(context);
            ViewBag.CurrencyId = new SelectList(currencyBusiness.GetCurrencyList(), "CurrencyId", "CurrencyCode", budget.CurrencyId);
            return View(budget);
        }

        // POST: Budget/Edit/5
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

                bool success = budgetBusiness.UpdateBudget(budget.BudgetId, budget.Name, budget.CurrencyId, UserId);
                //TODO: Operate on success
                //return ReturnUnauthorized(UNAUTHORIZED_MESSAGE);

                return RedirectToAction("Index");
            }

            CurrencyBusiness currencyBusiness = new CurrencyBusiness(context);
            ViewBag.CurrencyId = new SelectList(currencyBusiness.GetCurrencyList(), "CurrencyId", "CurrencyCode", budget.CurrencyId);
            return View(budget);
        }

        // GET: Budget/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Budget/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bool success = budgetBusiness.DeleteBudget(id, UserId);
            if(!success)
            {
                //TODO: return ReturnUnauthorized(UNAUTHORIZED_MESSAGE);
                return HttpNotFound();
            }

            return RedirectToAction("Index");
        }
    }
}
