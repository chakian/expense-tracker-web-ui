using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Context.DbModels;
using System;

namespace ExpenseTracker.Business
{
    public class BudgetUserBusiness : BaseBusiness
    {
        #region constructor
        public BudgetUserBusiness() : base() { }

        public BudgetUserBusiness(ExpenseTrackerContext context) : base(context) { }
        #endregion

        #region Private Methods
        #endregion

        #region Internal Methods
        #endregion

        internal void CreateBudgetUser_NoCommit(int budgetId, string userId, string insertUserId)
        {
            BudgetUser budgetUser = new BudgetUser
            {
                InsertUserId = insertUserId,
                InsertTime = DateTime.Now,
                UpdateUserId = insertUserId,
                UpdateTime = DateTime.Now,
                IsActive = true,

                BudgetId = budgetId,
                UserId = userId
            };

            context.BudgetUsers.Add(budgetUser);
        }
    }
}
