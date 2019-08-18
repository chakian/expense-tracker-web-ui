using System;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using ExpenseTracker.Business;
using ExpenseTracker.Entities;
using Microsoft.AspNet.Identity;

namespace ExpenseTracker.WebUI.Controllers
{
    public class CategoryController : BaseAuthenticatedController
    {
        private readonly BudgetCategoryBusiness budgetCategoryBusiness;
        private readonly CategoryBusiness categoryBusiness;

        public CategoryController()
        {
            budgetCategoryBusiness = new BudgetCategoryBusiness();
            categoryBusiness = new CategoryBusiness();
        }

        public ActionResult Index()
        {
            var categories = budgetCategoryBusiness.GetCategoriesOfUser(UserId, ActiveBudgetId);
            return View(categories);
        }

        public ActionResult Create() => View();

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "CategoryId,Name,BudgetId")] Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        categoryBusiness.CreateCategory(new CategoryEntity()
        //        {
        //            Name = category.Name,
        //            BudgetId = ActiveBudgetId,
        //            ParentId = category.ParentCategoryId
        //        }, UserId);

        //        return RedirectToAction("Index");
        //    }

        //    return View(category);
        //}

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Category category = budgetCategoryBusiness.GetCategoryById(id.Value, UserId);
            //if (category == null)
            //{
            //    return HttpNotFound();
            //}

            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "CategoryId,Name,BudgetId,InsertUserId,InsertTime,IsActive")] Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        category.UpdateUserId = User.Identity.GetUserId();
        //        category.UpdateTime = DateTime.Now;

        //        //TODO: Do not use context in Web project. Use the business methods instead!
        //        //context.Entry(category).State = EntityState.Modified;
        //        //context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(category);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int CategoryId)
        {
            //Category category = context.Categories.Find(CategoryId);
            //category.UpdateUserId = User.Identity.GetUserId();
            //category.UpdateTime = DateTime.Now;
            //category.IsActive = false;

            ////TODO: Do not use context in Web project. Use the business methods instead!
            //context.Entry(category).State = EntityState.Modified;
            //context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
