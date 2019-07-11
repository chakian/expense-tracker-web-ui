using System;
using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Context.DbModels;

namespace ExpenseTracker.Business
{
    public class TransactionTemplateBusiness : BaseBusiness
    {
        public TransactionTemplateBusiness(ExpenseTrackerContext context) : base(context)
        {
        }

        public bool CreateTransactionTemplate(string templateName, decimal? amount, string description, int? categoryId, int? sourceAccountId, int? targetAccountId, int budgetId, string userId)
        {
            try
            {
                TransactionTemplate transactionTemplate = new TransactionTemplate
                {
                    InsertUserId = userId,
                    InsertTime = DateTime.Now,
                    UpdateUserId = userId,
                    UpdateTime = DateTime.Now,
                    IsActive = true,

                    Name = templateName,
                    Amount = amount,
                    Description = description,
                    CategoryId = categoryId,
                    SourceAccountId = sourceAccountId,
                    TargetAccountId = targetAccountId,
                    BudgetId = budgetId,
                    UserId = userId
                };
                context.TransactionTemplates.Add(transactionTemplate);
                context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
