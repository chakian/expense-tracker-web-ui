using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ExpenseTracker.Entities;
using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Context.DbModels;

namespace ExpenseTracker.Business
{
    public class AccountBusiness : BaseBusiness
    {
        #region constructor
        public AccountBusiness() { }

        public AccountBusiness(ExpenseTrackerContext context) : base(context) { }
        #endregion

        #region Private Methods
        private Account GetAccountByIdInternal(int accountId)
        {
            Account account = context.Accounts.Find(accountId);
            return account;
        }
        #endregion

        #region Internal Methods
        #endregion

        public AccountEntity GetAccountById(int accountId, string userId)
        {
            Account account = GetAccountByIdInternal(accountId);

            if (account == null || !account.Budget.BudgetUsers.Any(bu => bu.UserId.Equals(userId)))
            {
                return null;
            }

            AccountEntity accountEntity = mapper.Map<AccountEntity>(account);

            return accountEntity;
        }

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

        public void CreateAccount(AccountEntity accountEntity, string userId)
        {
            Account account = new Account
            {
                InsertUserId = userId,
                InsertTime = DateTime.Now,
                UpdateUserId = userId,
                UpdateTime = DateTime.Now,
                IsActive = true,

                Name = accountEntity.Name,
                StartingBalance = accountEntity.StartingBalance,
                CurrentBalance = accountEntity.StartingBalance,
                BudgetId = accountEntity.BudgetId,
                AccountTypeId = accountEntity.AccountTypeId
            };

            context.Accounts.Add(account);
        }

        public AccountEntity UpdateAccount(AccountEntity accountEntity, string userId)
        {
            Account account = GetAccountByIdInternal(accountEntity.AccountId);

            if (account == null || !account.Budget.BudgetUsers.Any(bu => bu.UserId.Equals(userId)))
            {
                return null;
            }

            account.UpdateUserId = userId;
            account.UpdateTime = DateTime.Now;

            account.Name = accountEntity.Name;
            account.CurrentBalance = accountEntity.StartingBalance;

            context.SaveChanges();

            accountEntity = mapper.Map<AccountEntity>(account);

            return accountEntity;
        }

        public bool DeleteAccount(int accountId, string userId)
        {
            Account account = GetAccountByIdInternal(accountId);

            //TODO: Do some meaningful validations
            if (account == null || !account.Budget.BudgetUsers.Any(bu => bu.UserId.Equals(userId)))
            {
                return false;
            }

            account.UpdateUserId = userId;
            account.UpdateTime = DateTime.Now;
            account.IsActive = false;

            context.Entry(account).State = EntityState.Modified;
            context.SaveChanges();

            return true;
        }
    }
}
