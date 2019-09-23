﻿using ExpenseTracker.Entities;
using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Context.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Business
{
    public class TransactionBusiness : BaseBusiness
    {
        #region constructor
        public TransactionBusiness() : base() { }

        public TransactionBusiness(ExpenseTrackerContext context) : base(context) { }
        #endregion

        #region Private Methods
        #endregion

        #region Internal Methods
        #endregion

        public void InsertTransaction(string userId, int categoryId, int accountId, decimal amount, string description, DateTime date)
        {
            Transaction transaction = new Transaction
            {
                InsertUserId = userId,
                InsertTime = DateTime.Now,
                UpdateUserId = userId,
                UpdateTime = DateTime.Now,
                IsActive = true,

                SourceAccountId = accountId,
                Amount = amount,
                CategoryId = categoryId,
                Date = date,
                Description = description
            };
            context.Transactions.Add(transaction);
            context.SaveChanges();
        }

        public List<TransactionEntity> GetTransactionsForPeriodByGivenDate(DateTime inputDate, string userId, int activeBudgetId)
        {
            DateTime startOfMonth = new DateTime(inputDate.Year, inputDate.Month, 1, 0, 0, 0);
            DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);

            if (startOfMonth > endOfMonth)
            {
                throw new Exception("End Date must be a later date than Start Date!");
            }

            startOfMonth = new DateTime(startOfMonth.Year, startOfMonth.Month, startOfMonth.Day, 0, 0, 0);
            endOfMonth = new DateTime(endOfMonth.Year, endOfMonth.Month, endOfMonth.Day, 23, 59, 59);

            var transactions = context.Transactions.Where(trx =>
                trx.IsActive &&
                trx.SourceAccount.BudgetId.Equals(activeBudgetId) &&
                trx.SourceAccount.Budget.BudgetUsers.Any(bu => bu.UserId.Equals(userId)) &&
                trx.Date >= startOfMonth &&
                trx.Date <= endOfMonth).ToList();

            List<Transaction> list = transactions;

            List<TransactionEntity> transactionEntities = new List<TransactionEntity>();
            list.ForEach(t =>
            {
                transactionEntities.Add(new TransactionEntity
                {
                    TransactionId = t.TransactionId,
                    Date = t.Date,
                    Description = t.Description,
                    Amount = t.Amount,
                    CategoryId = t.CategoryId,
                    SourceAccountId = t.SourceAccountId,
                    Category = new CategoryEntity
                    {
                        CategoryId = t.Category.CategoryId,
                        Name = t.Category.Name
                    },
                    SourceAccount = new AccountEntity
                    {
                        AccountId = t.SourceAccount.AccountId,
                        Name = t.SourceAccount.Name
                    }
                });
            });
            return transactionEntities;
        }

        public TransactionEntity InsertTransaction(TransactionEntity transactionEntity, string userId) => throw new NotImplementedException();

        public List<TransactionEntity> GetTransactionsForPeriodByGivenDate_GroupedByCategory(DateTime inputDate, string userId, int activeBudgetId)
        {
            var rawList = GetTransactionsForPeriodByGivenDate(inputDate, userId, activeBudgetId);
            List<TransactionEntity> groupedList = new List<TransactionEntity>();
            rawList.GroupBy(t => t.CategoryId).Select(t => new
            {
                Amount = t.Sum(q => q.Amount),
                t.First().CategoryId
            }).ToList().ForEach(g =>
            {
                groupedList.Add(new TransactionEntity
                {
                    CategoryId = g.CategoryId,
                    Amount = g.Amount
                });
            });

            return groupedList;
        }

        public TransactionEntity GetTransactionById(int transactionId, string userId)
        {
            var transaction = context.Transactions.Find(transactionId);
            if (!transaction.IsActive || !transaction.SourceAccount.Budget.BudgetUsers.Any(q => q.UserId.Equals(userId)))
            {
                return null;
            }

            return new TransactionEntity
            {
                Amount = transaction.Amount,
                //Category=transaction.Category,
                //SourceAccount=transaction.SourceAccount,
                CategoryId = transaction.CategoryId,
                Date = transaction.Date,
                Description = transaction.Description,
                SourceAccountId = transaction.SourceAccountId,
                TransactionId = transaction.TransactionId
            };
        }

        public void UpdateTransaction(int transactionId, string userId, int categoryId, int accountId, decimal amount, string description, DateTime date)
        {
            var transaction = context.Transactions.Find(transactionId);
            if (transaction.IsActive && transaction.SourceAccount.Budget.BudgetUsers.Any(q => q.UserId.Equals(userId)))
            {
                transaction.UpdateUserId = userId;
                transaction.UpdateTime = DateTime.Now;

                transaction.SourceAccountId = accountId;
                transaction.Amount = amount;
                transaction.CategoryId = categoryId;
                transaction.Date = date;
                transaction.Description = description;

                context.Entry(transaction).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteTransaction(int transactionId, string userId)
        {
            var transaction = context.Transactions.Find(transactionId);
            if (transaction.IsActive && transaction.SourceAccount.Budget.BudgetUsers.Any(q => q.UserId.Equals(userId)))
            {
                transaction.UpdateUserId = userId;
                transaction.UpdateTime = DateTime.Now;

                transaction.IsActive = false;

                context.Entry(transaction).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
