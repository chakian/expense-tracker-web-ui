using ExpenseTracker.Entities;
using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Context.DbModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ExpenseTracker.Business
{
    public class BudgetCategoryBusiness : BaseBusiness
    {
        #region constructor
        public BudgetCategoryBusiness() : base() { }

        public BudgetCategoryBusiness(ExpenseTrackerContext context) : base(context) { }
        #endregion

        #region Private Methods
        private Category GetCategoryById(int categoryId, string userId)
        {
            Category category = context.Categories.Find(categoryId);

            if (category == null || !category.Budget.BudgetUsers.Any(bu => bu.UserId.Equals(userId)))
            {
                return null;
            }

            return category;
        }
        #endregion

        #region Internal Methods
        #endregion

        public List<CategoryEntity> GetCategoriesOfUser(string userId, int budgetId)
        {
            var contextObjectList = context.Categories.Where(a => a.IsActive && a.BudgetId == budgetId && a.Budget.BudgetUsers.Any(bu => bu.UserId.Equals(userId)))
                .Include(a => a.Budget)
                .Include(a => a.InsertUser)
                .Include(a => a.UpdateUser)
                .ToList();

            return null;
        }
    }
}
