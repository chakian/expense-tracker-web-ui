using System;
using System.Collections.Generic;
using System.Linq;
using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Context.DbModels;

namespace ExpenseTracker.Business
{
    public class BudgetPlanCategoryBusiness : BaseBusiness
    {
        public BudgetPlanCategoryBusiness() : base() { }

        public BudgetPlanCategoryBusiness(ExpenseTrackerContext context) : base(context) { }

        public List<BudgetPlanCategory> GetBudgetPlanCategoriesByPlanId(int budgetPlanId, string userId)
        {
            InsertMissingCategoriesToBudgetPlan(budgetPlanId, userId);
            var categories = context.BudgetPlanCategories.Where(q => q.IsActive && q.BudgetPlanId.Equals(budgetPlanId)).ToList();
            return categories;
        }

        private void InsertMissingCategoriesToBudgetPlan(int budgetPlanId, string userId)
        {
            var budgetId = context.BudgetPlans.First(q => q.BudgetPlanId.Equals(budgetPlanId)).BudgetId;
            var planCategories = context.BudgetPlanCategories.Where(q => q.IsActive && q.BudgetPlanId.Equals(budgetPlanId)).ToList();
            var categories = new BudgetCategoryBusiness().GetCategoriesOfUser(userId, budgetId);

            foreach (var category in categories)
            {
                if (!planCategories.Any(q => q.CategoryId.Equals(category.CategoryId)))
                {
                    context.BudgetPlanCategories.Add(new BudgetPlanCategory()
                    {
                        InsertUserId = userId,
                        InsertTime = DateTime.Now,
                        UpdateUserId = userId,
                        UpdateTime = DateTime.Now,
                        IsActive = true,

                        BudgetPlanId = budgetPlanId,
                        CategoryId = category.CategoryId,
                        PlannedAmount = 0
                    });
                }
            }
            context.SaveChanges();
        }
    }
}
