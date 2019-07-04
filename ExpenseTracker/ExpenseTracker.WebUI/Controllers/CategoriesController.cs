using System;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using ExpenseTracker.Business;
using ExpenseTracker.Persistence.Context.DbModels;
using Microsoft.AspNet.Identity;

namespace ExpenseTracker.WebUI.Controllers
{
    public class CategoriesController : BaseAuthenticatedController
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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,Name,BudgetId")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.BudgetId = ActiveBudgetId;

                category.InsertUserId = User.Identity.GetUserId();
                category.InsertTime = DateTime.Now;
                category.UpdateUserId = User.Identity.GetUserId();
                category.UpdateTime = DateTime.Now;
                category.IsActive = true;

                context.Categories.Add(category);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

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

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int CategoryId)
        {
            Category category = context.Categories.Find(CategoryId);
            category.UpdateUserId = User.Identity.GetUserId();
            category.UpdateTime = DateTime.Now;
            category.IsActive = false;

            context.Entry(category).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
