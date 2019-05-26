using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Context.DbModels;
using System;

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

                SourceAccountId=accountId,
                Amount=amount,
                CategoryId=categoryId,
                Date=date,
                Description=description
            };
            context.Transactions.Add(transaction);
            context.SaveChanges();
        }
    }
}
