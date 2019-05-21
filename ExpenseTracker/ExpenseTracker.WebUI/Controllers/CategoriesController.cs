using System;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using ExpenseTracker.Business;
using ExpenseTracker.Persistence.Context.DbModels;
using Microsoft.AspNet.Identity;

namespace ExpenseTracker.WebUI.Controllers
{
    [Authorize]
    public class CategoriesController : BaseController
    {
        private readonly BudgetCategoryBusiness budgetCategoryBusiness;

        public CategoriesController()
        {
            budgetCategoryBusiness = new BudgetCategoryBusiness(context);
        }

        public ActionResult Index()
        {
            var categories = budgetCategoryBusiness.GetCategoriesOfUser(UserId, ActiveBudgetId);
            return View(categories);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = budgetCategoryBusiness.GetCategoryById(id.Value, UserId);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        public ActionResult Create()
        {
            SetViewBagValues(null);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,Name,BudgetId")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.InsertUserId = User.Identity.GetUserId();
                category.InsertTime = DateTime.Now;
                category.UpdateUserId = User.Identity.GetUserId();
                category.UpdateTime = DateTime.Now;
                category.IsActive = true;

                context.Categories.Add(category);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            SetViewBagValues(category.BudgetId);
            return View(category);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = budgetCategoryBusiness.GetCategoryById(id.Value, UserId);
            if (category == null)
            {
                return HttpNotFound();
            }

            SetViewBagValues(category.BudgetId);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,Name,BudgetId,InsertUserId,InsertTime,IsActive")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.UpdateUserId = User.Identity.GetUserId();
                category.UpdateTime = DateTime.Now;

                context.Entry(category).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            SetViewBagValues(category.BudgetId);
            return View(category);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = budgetCategoryBusiness.GetCategoryById(id.Value, UserId);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = context.Categories.Find(id);
            category.UpdateUserId = User.Identity.GetUserId();
            category.UpdateTime = DateTime.Now;
            category.IsActive = false;

            context.Entry(category).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        private void SetViewBagValues(int? selectedBudgetId)
        {
            BudgetBusiness budgetBusiness = new BudgetBusiness(context);
            ViewBag.BudgetId = new SelectList(budgetBusiness.GetBudgetsOfUser(UserId), "BudgetId", "Name", selectedBudgetId);
        }
    }
}
