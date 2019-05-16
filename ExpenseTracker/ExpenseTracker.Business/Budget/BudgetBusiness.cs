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
    }
}
