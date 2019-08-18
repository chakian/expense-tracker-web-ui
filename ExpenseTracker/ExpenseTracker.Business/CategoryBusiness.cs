using ExpenseTracker.Entities;
using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Context.DbModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ExpenseTracker.Business
{
    public class CategoryBusiness : BaseBusiness
    {
        #region constructor
        public CategoryBusiness() : base() { }

        public CategoryBusiness(ExpenseTrackerContext context) : base(context) { }
        #endregion

        #region Private Methods
        private Category ConvertEntityToDbo(CategoryEntity entity, string userId)
        {
            Category category = CreateAuditableContextObject<Category>(userId);
            category.Name = entity.Name;
            category.BudgetId = entity.BudgetId;
            category.IsIncomeCategory = entity.IsIncomeCategory;
            category.ParentCategoryId = entity.ParentCategoryId;
            return category;
        }

        private CategoryEntity ConvertDboToEntity(Category dbo)
        {
            CategoryEntity category = CreateAuditableEntityObject<CategoryEntity>(dbo);
            category.CategoryId = dbo.CategoryId;
            category.Name = dbo.Name;
            category.BudgetId = dbo.BudgetId;
            category.IsIncomeCategory = dbo.IsIncomeCategory;
            category.ParentCategoryId = dbo.ParentCategoryId;
            return category;
        }

        private List<Category> GetCategoriesByBudgetIdInternal(int budgetId, string userId)
        {
            var categories = context.Categories.Where(q => q.IsActive && q.BudgetId.Equals(budgetId) && q.Budget.BudgetUsers.Any(bu => bu.UserId.Equals(userId))).ToList();
            return categories;
        }
        #endregion

        #region Internal Methods
        #endregion

        public CategoryEntity CreateCategory(CategoryEntity categoryEntity, string userId)
        {
            //TODO: Validations!!!
            if (GetCategoriesByBudgetId(categoryEntity.BudgetId, userId).Any(q => q.Name.Equals(categoryEntity.Name)))
            {
                return null;
            }
            if (categoryEntity.ParentCategoryId == 0)
            {
                categoryEntity.ParentCategoryId = null;
            }
            if (categoryEntity.ParentCategoryId.HasValue && !GetCategoriesByBudgetId(categoryEntity.BudgetId, userId).Any(q => q.CategoryId.Equals(categoryEntity.ParentCategoryId)))
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
            Category category = GetCategoriesByBudgetIdInternal(categoryEntity.BudgetId, userId).SingleOrDefault(q => q.CategoryId.Equals(categoryEntity.CategoryId));
            //TODO: Validations!!!
            if (category == null)
            {
                return null;
            }
            if (categoryEntity.ParentCategoryId == 0)
            {
                categoryEntity.ParentCategoryId = null;
            }
            if (categoryEntity.ParentCategoryId.HasValue && !GetCategoriesByBudgetId(categoryEntity.BudgetId, userId).Any(q => q.CategoryId.Equals(categoryEntity.ParentCategoryId)))
            {
                return null;
            }

            //TODO: Empty name should be validated above
            if (!string.IsNullOrEmpty(categoryEntity.Name))
            {
                category.Name = categoryEntity.Name;
            }
            category.IsIncomeCategory = categoryEntity.IsIncomeCategory;
            category.ParentCategoryId = categoryEntity.ParentCategoryId;

            category.UpdateUserId = userId;
            category.UpdateTime = DateTime.Now;

            context.Entry(category).State = EntityState.Modified;

            context.SaveChanges();

            categoryEntity = ConvertDboToEntity(category);
            return categoryEntity;
        }

        public bool DeleteCategory(int categoryId, string userId)
        {
            Category category = context.Categories.Find(categoryId);

            if (category == null || !category.Budget.BudgetUsers.Any(bu => bu.UserId.Equals(userId)))
            {
                return false;
            }

            if (GetCategoriesByBudgetIdInternal(category.BudgetId, userId).Any(q => q.ParentCategoryId.Equals(categoryId)))
            {
                return false;
            }

            category.UpdateUserId = userId;
            category.UpdateTime = DateTime.Now;
            category.IsActive = false;

            context.Entry(category).State = EntityState.Modified;
            context.SaveChanges();

            return true;
        }

        public List<CategoryEntity> GetCategoriesByBudgetId(int budgetId, string userId)
        {
            var categories = context.Categories.Where(q => q.IsActive && q.BudgetId.Equals(budgetId) && q.Budget.BudgetUsers.Any(bu => bu.UserId.Equals(userId))).ToList();
            var entityList = new List<CategoryEntity>();

            categories.ForEach(c =>
            {
                entityList.Add(new CategoryEntity
                {
                    CategoryId = c.CategoryId,
                    Name = c.Name
                });
            });

            return entityList;
        }

        public List<CategoryEntity> GetCategoriesOfUser(string userId, int budgetId)
        {
            List<Category> contextObjectList = context.Categories.Where(a => a.IsActive && a.BudgetId == budgetId && a.Budget.BudgetUsers.Any(bu => bu.UserId.Equals(userId)))
                .Include(a => a.Budget)
                .Include(a => a.InsertUser)
                .Include(a => a.UpdateUser)
                .ToList();

            List<CategoryEntity> categoryEntities = mapper.Map<List<CategoryEntity>>(contextObjectList);

            return categoryEntities;
        }

        public CategoryEntity GetCategoryById(int categoryId, string userId)
        {
            Category category = context.Categories.Find(categoryId);

            if (category == null || !category.Budget.BudgetUsers.Any(bu => bu.UserId.Equals(userId)))
            {
                return null;
            }

            CategoryEntity categoryEntity = mapper.Map<CategoryEntity>(category);

            return categoryEntity;
        }
    }
}
