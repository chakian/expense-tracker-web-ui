using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Context.DbModels;
using System.Collections.Generic;

namespace ExpenseTracker.Business
{
    public class BudgetBusiness : BaseBusiness
    {
        public BudgetBusiness(ExpenseTrackerContext context) : base(context) { }

        public List<Budget> GetBudgetsOfUser(string userId)
        {
            throw new System.Exception();a
        }
    }
}
