using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Context.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Business
{
    public class BudgetPlanBusiness : BaseBusiness
    {
        public BudgetPlanBusiness() : base() { }

        public BudgetPlanBusiness(ExpenseTrackerContext context) : base(context) { }

        public BudgetPlan GetBudgetPlanById(int budgetPlanId, string userId)
        {
            BudgetPlan budgetPlan = context.BudgetPlans.Find(budgetPlanId);
            budgetPlan.Budget = context.Budgets.Find(budgetPlan.BudgetId);

            if (budgetPlan == null || !budgetPlan.Budget.BudgetUsers.Any(bp => bp.UserId.Equals(userId)))
            {
                return null;
            }

            budgetPlan.BudgetPlanCategories = new BudgetPlanCategoryBusiness(context).GetBudgetPlanCategoriesByPlanId(budgetPlan.BudgetPlanId, userId);

            //budgetPlan.InsertUser = context.Users.Find(budgetPlan.InsertUserId);
            //budgetPlan.UpdateUser = context.Users.Find(budgetPlan.UpdateUserId);

            return budgetPlan;
        }

        public BudgetPlan GetBudgetPlanByYearAndMonth(int budgetId, int year, int month, string userId)
        {
            BudgetPlan budgetPlan = context.BudgetPlans.FirstOrDefault(bp => bp.IsActive && bp.BudgetId.Equals(budgetId) && bp.Year.Equals(year) && bp.Month.Equals(month));

            if (budgetPlan == null || !budgetPlan.Budget.BudgetUsers.Any(bp => bp.UserId.Equals(userId)))
            {
                if (IsRequestedDateEqualToCurrentDate(year, month) || IsRequestedDateAdjacentToAnExistingBudgetPlanPeriod(budgetId, year, month, userId))
                {
                    int newBudgetPlanId = CreateBudgetPlanForPeriod(budgetId, year, month, userId);
                    return GetBudgetPlanById(newBudgetPlanId, userId);
                }
                else
                {
                    return null;
                }
            }

            budgetPlan.BudgetPlanCategories = new BudgetPlanCategoryBusiness(context).GetBudgetPlanCategoriesByPlanId(budgetPlan.BudgetPlanId, userId);

            //budgetPlan.InsertUser = context.Users.Find(budgetPlan.InsertUserId);
            //budgetPlan.UpdateUser = context.Users.Find(budgetPlan.UpdateUserId);

            return budgetPlan;
        }

        private BudgetPlan GetBudgetPlanByYearAndMonth_NonRecursive(int budgetId, int year, int month, string userId)
        {
            BudgetPlan budgetPlan = context.BudgetPlans.FirstOrDefault(bp => bp.IsActive && bp.BudgetId.Equals(budgetId) && bp.Year.Equals(year) && bp.Month.Equals(month));

            if (budgetPlan == null || !budgetPlan.Budget.BudgetUsers.Any(bp => bp.UserId.Equals(userId)))
            {
                return null;
            }

            budgetPlan.BudgetPlanCategories = new BudgetPlanCategoryBusiness(context).GetBudgetPlanCategoriesByPlanId(budgetPlan.BudgetPlanId, userId);

            //budgetPlan.InsertUser = context.Users.Find(budgetPlan.InsertUserId);
            //budgetPlan.UpdateUser = context.Users.Find(budgetPlan.UpdateUserId);

            return budgetPlan;
        }

        private bool IsRequestedDateEqualToCurrentDate(int year, int month) => DateTime.Now.Year.Equals(year) && DateTime.Now.Month.Equals(month);

        private bool IsRequestedDateAdjacentToAnExistingBudgetPlanPeriod(int budgetId, int year, int month, string userId)
        {
            DateTime requestedDate = new DateTime(year, month, 1);
            DateTime previousDate = requestedDate.AddMonths(-1);
            DateTime nextDate = requestedDate.AddMonths(1);
            if (GetBudgetPlanByYearAndMonth_NonRecursive(budgetId, previousDate.Year, previousDate.Month, userId) != null)
            {
                return true;
            }
            else if (GetBudgetPlanByYearAndMonth_NonRecursive(budgetId, nextDate.Year, nextDate.Month, userId) != null)
            {
                return true;
            }
            return false;
        }

        private int CreateBudgetPlanForPeriod(int budgetId, int year, int month, string userId)
        {
            BudgetPlan budgetPlan = new BudgetPlan
            {
                InsertUserId = userId,
                InsertTime = DateTime.Now,
                UpdateUserId = userId,
                UpdateTime = DateTime.Now,
                IsActive = true,

                BudgetId = budgetId,
                Year = year,
                Month = month
            };

            context.BudgetPlans.Add(budgetPlan);
            context.SaveChanges();

            return budgetPlan.BudgetPlanId;
        }

        public void UpdatePlan(List<BudgetPlanCategory> budgetPlanCategories, string userId)
        {
            foreach (var category in budgetPlanCategories)
            {
                BudgetPlanCategory budgetPlanCategory = context.BudgetPlanCategories.Find(category.BudgetPlanCategoryId);
                budgetPlanCategory.PlannedAmount = category.PlannedAmount;

                budgetPlanCategory.UpdateTime = DateTime.Now;
                budgetPlanCategory.UpdateUserId = userId;

                context.SaveChanges();
            }
        }
    }
}
