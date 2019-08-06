using ExpenseTracker.Entities;
using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Context.DbModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace ExpenseTracker.Business
{
    public class CategoryBusiness : BaseBusiness
    {
        public CategoryBusiness(ExpenseTrackerContext context) : base(context) { }

        private Category ConvertEntityToDbo(CategoryEntity entity, string userId)
        {
            Category category = CreateAuditableContextObject<Category>(userId);
            category.Name = entity.Name;
            category.BudgetId = entity.BudgetId;
            category.IsIncomeCategory = entity.IsIncomeCategory;
            category.ParentCategoryId = entity.ParentId;
            return category;
        }

        private CategoryEntity ConvertDboToEntity(Category dbo)
        {
            CategoryEntity category = CreateAuditableEntityObject<CategoryEntity>(dbo);
            category.CategoryId = dbo.CategoryId;
            category.Name = dbo.Name;
            category.BudgetId = dbo.BudgetId;
            category.IsIncomeCategory = dbo.IsIncomeCategory;
            category.ParentId = dbo.ParentCategoryId;
            return category;
        }

        private Category GetCategoryById(int categoryId, string userId)
        {
            var category = context.Categories.SingleOrDefault(q => q.IsActive && q.CategoryId.Equals(categoryId) && q.Budget.BudgetUsers.Any(bu => bu.UserId.Equals(userId)));
            return category;
        }

        public CategoryEntity CreateCategory(CategoryEntity categoryEntity, string userId)
        {
            //TODO: Validations!!!
            if(GetCategoriesByBudgetId(categoryEntity.BudgetId, userId).Any(q => q.Name.Equals(categoryEntity.Name)))
            {
                return null;
            }
            if(categoryEntity.ParentId.HasValue && !GetCategoriesByBudgetId(categoryEntity.BudgetId, userId).Any(q => q.CategoryId.Equals(categoryEntity.ParentId)))
            {
                return null;
            }

            Category category = ConvertEntityToDbo(categoryEntity, userId);
            
            context.Categories.Add(category);
            context.SaveChanges();

            categoryEntity = ConvertDboToEntity(category);
            return categoryEntity;
        }

        public CategoryEntity UpdateCategory(CategoryEntity categoryEntity, string userId)
        {
            var category = GetCategoriesByBudgetId(categoryEntity.BudgetId, userId).SingleOrDefault(q => q.CategoryId.Equals(categoryEntity.CategoryId));
            //TODO: Validations!!!
            if (category == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(categoryEntity.Name))
            {
                category.Name = categoryEntity.Name;
            }
            category.IsIncomeCategory = categoryEntity.IsIncomeCategory;

            context.SaveChanges();

            categoryEntity = ConvertDboToEntity(category);
            return categoryEntity;
        }

        public bool DeleteCategory(int categoryId, string userId)
        {
            var category = GetCategoryById(categoryId, userId);
            if(category == null)
            {
                return false;
            }

            category.IsActive = false;
            context.SaveChanges();

            return true;
        }

        //TODO: Use Entities!
        public List<Category> GetCategoriesByBudgetId(int budgetId, string userId)
        {
            var categories = context.Categories.Where(q => q.IsActive && q.BudgetId.Equals(budgetId) && q.Budget.BudgetUsers.Any(bu => bu.UserId.Equals(userId))).ToList();
            return categories;
        }
    }
}
