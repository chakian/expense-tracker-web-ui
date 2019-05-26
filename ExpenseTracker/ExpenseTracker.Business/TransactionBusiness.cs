﻿using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Context.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Business
{
    public class TransactionBusiness : BaseBusiness
    {
        public TransactionBusiness(ExpenseTrackerContext context) : base(context) { }

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

        public List<Transaction> GetTransactionsForCurrentPeriod(string userId, int activeBudgetId)
        {
            DateTime today = DateTime.Now;
            DateTime startOfMonth = new DateTime(today.Year, today.Month, 1, 0, 0, 0);
            DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);

            var transactions = context.Transactions.Where(trx =>
                trx.IsActive &&
                trx.SourceAccount.BudgetId.Equals(activeBudgetId) &&
                trx.SourceAccount.Budget.BudgetUsers.Any(bu => bu.UserId.Equals(userId)) &&
                trx.Date >= startOfMonth &&
                trx.Date <= endOfMonth).ToList();

            return transactions;
        }

        public Transaction GetTransactionById(int transactionId, string userId)
        {
            var transaction = context.Transactions.Find(transactionId);
            if (!transaction.IsActive || !transaction.SourceAccount.Budget.BudgetUsers.Any(q => q.UserId.Equals(userId)))
            {
                return null;
            }

            return transaction;
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
