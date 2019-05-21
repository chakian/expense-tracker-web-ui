using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Context.DbModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ExpenseTracker.Business
{
    public class BudgetCategoryBusiness : BaseBusiness
    {
        public BudgetCategoryBusiness(ExpenseTrackerContext context) : base(context)
        {
        }

        public List<Category> GetCategoriesOfUser(string userId, int budgetId) => context.Categories.Where(a => a.BudgetId == budgetId && a.Budget.BudgetUsers.Any(bu => bu.UserId.Equals(userId)))
            .Include(a => a.Budget)
            .Include(a => a.InsertUser)
            .Include(a => a.UpdateUser)
            .ToList();

        public Category GetCategoryById(int categoryId, string userId)
        {
            Category category = context.Categories.Find(categoryId);

            if (category == null || !category.Budget.BudgetUsers.Any(bu => bu.UserId.Equals(userId)))
            {
                return null;
            }

            category.InsertUser = context.Users.Find(category.InsertUserId);
            category.UpdateUser = context.Users.Find(category.UpdateUserId);

            return category;
        }
    }
}
