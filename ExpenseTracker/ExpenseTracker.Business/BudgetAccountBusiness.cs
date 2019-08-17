using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ExpenseTracker.Entities;
using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Context.DbModels;

namespace ExpenseTracker.Business
{
    public class BudgetAccountBusiness : BaseBusiness
    {
        #region constructor
        public BudgetAccountBusiness() { }

        public BudgetAccountBusiness(ExpenseTrackerContext context) : base(context) { }
        #endregion

        #region Private Methods
        private Account GetAccountById(int accountId, string userId)
        {
            Account account = context.Accounts.Find(accountId);

            if (account == null || !account.Budget.BudgetUsers.Any(bu => bu.UserId.Equals(userId)))
            {
                return null;
            }

            //account.InsertUser = context.Users.Find(account.InsertUserId);
            //account.UpdateUser = context.Users.Find(account.UpdateUserId);

            return account;
        }
        #endregion

        #region Internal Methods
        #endregion

        public List<AccountEntity> GetAccountsOfUser(string userId, int budgetId)
        {
            var list = context.Accounts.Where(a => a.IsActive && a.BudgetId == budgetId && a.Budget.BudgetUsers.Any(bu => bu.UserId.Equals(userId)))
                .Include(a => a.AccountType)
                .Include(a => a.Budget)
                .ToList();

            List<AccountEntity> accountEntities = new List<AccountEntity>();

            list.ForEach(a =>
            {
                accountEntities.Add(new AccountEntity()
                {
                    AccountId = a.AccountId,
                    Name = a.Name
                });
            });

            return accountEntities;
        }
    }
}
