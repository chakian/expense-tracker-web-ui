using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Context.DbModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ExpenseTracker.Business
{
    public class BudgetBusiness : BaseBusiness
    {
        public BudgetBusiness(ExpenseTrackerContext context) : base(context) { }

        public List<Budget> GetBudgetsOfUser(string userId)
        {
            return context.Budgets.Where(b => b.IsActive && b.BudgetUsers.Any(bu => bu.IsActive && bu.UserId.Equals(userId)))
                .Include(b => b.Currency)
                .ToList();
        }

        public Budget GetBudgetDetails(int budgetId, string userId)
        {
            Budget budget = context.Budgets.Find(budgetId);

            if (budget == null || !budget.BudgetUsers.Any(bu => bu.UserId.Equals(userId)))
            {
                return null;
            }

            budget.InsertUser = context.Users.Find(budget.InsertUserId);
            budget.UpdateUser = context.Users.Find(budget.UpdateUserId);

            return budget;
        }
    }
}
