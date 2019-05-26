using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Context.DbModels;
using System;

namespace ExpenseTracker.Business
{
    public class BudgetUserBusiness : BaseBusiness
    {
        public BudgetUserBusiness(ExpenseTrackerContext context) : base(context) { }

        internal void CreateBudgetUser_NoCommit(int budgetId, string userId, string insertUserId)
        {
            BudgetUser budgetUser = new BudgetUser();
            budgetUser.InsertUserId = insertUserId;
            budgetUser.InsertTime = DateTime.Now;
            budgetUser.UpdateUserId = insertUserId;
            budgetUser.UpdateTime = DateTime.Now;
            budgetUser.IsActive = true;

            budgetUser.BudgetId = budgetId;
            budgetUser.UserId = userId;

            context.BudgetUsers.Add(budgetUser);
        }
    }
}
