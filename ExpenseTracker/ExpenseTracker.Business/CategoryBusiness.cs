using ExpenseTracker.Entities;
using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Context.DbModels;
using System.Collections.Generic;
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
            return category;
        }

        private CategoryEntity ConvertDboToEntity(Category dbo)
        {
            CategoryEntity category = CreateAuditableEntityObject<CategoryEntity>(dbo);
            category.CategoryId = dbo.CategoryId;
            category.Name = dbo.Name;
            category.BudgetId = dbo.BudgetId;
            category.IsIncomeCategory = dbo.IsIncomeCategory;
            return category;
        }

        public CategoryEntity CreateCategory(CategoryEntity categoryEntity, string userId)
        {
            //TODO: Validations!!!
            if(GetCategoriesByBudgetId(categoryEntity.BudgetId, userId).Any(q => q.Name.Equals(categoryEntity.Name)))
            {
                return null;
            }

            Category category = ConvertEntityToDbo(categoryEntity, userId);
            
            context.Categories.Add(category);
            context.SaveChanges();

            categoryEntity = ConvertDboToEntity(category);
            return categoryEntity;
        }

        //TODO: Use Entities!
        public List<Category> GetCategoriesByBudgetId(int budgetId, string userId)
        {
            var categories = context.Categories.Where(q => q.IsActive && q.BudgetId.Equals(budgetId) && q.Budget.BudgetUsers.Any(bu => bu.UserId.Equals(userId))).ToList();
            return categories;
        }
    }
}
