using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using ExpenseTracker.Business;
using ExpenseTracker.Entities;
using ExpenseTracker.WebUI.Models.Category;

namespace ExpenseTracker.WebUI.Controllers
{
    public class CategoryController : BaseAuthenticatedController
    {
        private readonly CategoryBusiness categoryBusiness;

        public CategoryController()
        {
            categoryBusiness = new CategoryBusiness();
        }

        public ActionResult Index()
        {
            List<CategoryEntity> categories = categoryBusiness.GetCategoriesOfUser(UserId, ActiveBudgetId);
            return View(categories);
        }

        public ActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                categoryBusiness.CreateCategory(new CategoryEntity
                {
                    Name = model.Name,
                    BudgetId = ActiveBudgetId,
                    ParentCategoryId = model.ParentCategoryId
                }, UserId);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryEntity category = categoryBusiness.GetCategoryById(id.Value, UserId);
            if (category == null)
            {
                return HttpNotFound();
            }

            CreateCategoryModel model = new CreateCategoryModel
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                BudgetId = category.BudgetId,
                ParentCategoryId = category.ParentCategoryId ?? 0
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                categoryBusiness.UpdateCategory(new CategoryEntity
                {
                    CategoryId = model.CategoryId,
                    Name = model.Name,
                    BudgetId = model.BudgetId,
                    ParentCategoryId = model.ParentCategoryId
                }, UserId);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int CategoryId)
        {
            categoryBusiness.DeleteCategory(CategoryId, UserId);

            return RedirectToAction("Index");
        }
    }
}
