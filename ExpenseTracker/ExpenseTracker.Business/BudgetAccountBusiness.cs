using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Context.DbModels;

namespace ExpenseTracker.Business
{
    public class BudgetAccountBusiness : BaseBusiness
    {
        public BudgetAccountBusiness(ExpenseTrackerContext context) : base(context)
        {
        }

        public List<Account> GetAccountsOfUser(string userId, int budgetId) => context.Accounts.Where(a => a.BudgetId == budgetId && a.Budget.BudgetUsers.Any(bu => bu.UserId.Equals(userId)))
                .Include(a => a.AccountType)
                .Include(a => a.Budget)
                .ToList();

        public Account GetAccountById(int accountId, string userId)
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
    }
}
