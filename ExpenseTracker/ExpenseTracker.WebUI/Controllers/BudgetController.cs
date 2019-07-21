using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using ExpenseTracker.Business;
using ExpenseTracker.Persistence.Context.DbModels;
using ExpenseTracker.WebUI.Models.BudgetRelated;

namespace ExpenseTracker.WebUI.Controllers
{
    public class BudgetController : BaseAuthenticatedController
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
            BudgetListModel model = new BudgetListModel
            {
                BudgetList = new List<BasicBudgetInfo>()
            };

            List<Budget> budgets = budgetBusiness.GetBudgetsOfUser(UserId);
            budgets.ForEach(b =>
            {
                model.BudgetList.Add(new BasicBudgetInfo { Id = b.BudgetId, Name = b.Name, CurrencyDisplayName = b.Currency.DisplayName });
            });

            return View(model);
        }

        // GET: Budget/Create
        public ActionResult Create()
        {
            CurrencyBusiness currencyBusiness = new CurrencyBusiness(context);
            ViewBag.CurrencyId = new SelectList(currencyBusiness.GetCurrencyList(), "CurrencyId", "DisplayName");

            BudgetCreateModel model = new BudgetCreateModel();

            return View(model);
        }

        // POST: Budget/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BudgetCreateModel model)
        {
            if (ModelState.IsValid)
            {
                budgetBusiness.CreateBudget(model.Name, model.CurrencyId, UserId);
                return RedirectToAction("Index");
            }

            CurrencyBusiness currencyBusiness = new CurrencyBusiness(context);
            ViewBag.CurrencyId = new SelectList(currencyBusiness.GetCurrencyList(), "CurrencyId", "DisplayName", model.CurrencyId);
            return View(model);
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

            BudgetDetailModel model = new BudgetDetailModel
            {
                Id = budget.BudgetId,
                Name = budget.Name,
                CurrencyId = budget.CurrencyId
            };

            CurrencyBusiness currencyBusiness = new CurrencyBusiness(context);
            ViewBag.CurrencyId = new SelectList(currencyBusiness.GetCurrencyList(), "CurrencyId", "DisplayName", model.CurrencyId);

            return View(model);
        }

        // POST: Budget/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BudgetDetailModel model)
        {
            if (ModelState.IsValid)
            {
                bool success = budgetBusiness.UpdateBudget(model.Id, model.Name, model.CurrencyId, UserId);
                if (!success)
                {
                    return new HttpUnauthorizedResult();
                    //TODO: Operate on success
                    //return ReturnUnauthorized(UNAUTHORIZED_MESSAGE);
                }

                return RedirectToAction("Index");
            }

            CurrencyBusiness currencyBusiness = new CurrencyBusiness(context);
            ViewBag.CurrencyId = new SelectList(currencyBusiness.GetCurrencyList(), "CurrencyId", "DisplayName", model.CurrencyId);

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            bool success = budgetBusiness.DeleteBudget(id, UserId);
            if(!success)
            {
                //TODO: return ReturnUnauthorized(UNAUTHORIZED_MESSAGE);
                return HttpNotFound();
            }

            return RedirectToAction("Index");
        }

        public ActionResult SetActive(int budgetId)
        {
            Session["ActiveBudgetId"] = budgetId;
            Session["ActiveBudgetName"] = budgetBusiness.GetBudgetDetails(budgetId, UserId).Name;

            var user = context.Users.Find(UserId);
            user.ActiveBudgetId = budgetId;
            context.Entry(user).State = EntityState.Modified;
            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
