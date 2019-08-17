using System;
using System.Collections.Generic;
using System.Linq;
using ExpenseTracker.Entities;
using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Context.DbModels;

namespace ExpenseTracker.Business
{
    public class TransactionTemplateBusiness : BaseBusiness
    {
        #region constructor
        public TransactionTemplateBusiness() : base() { }

        public TransactionTemplateBusiness(ExpenseTrackerContext context) : base(context) { }
        #endregion

        #region Private Methods
        #endregion

        public bool CreateTransactionTemplate(string templateName, decimal? amount, string description, int? categoryId, int? sourceAccountId, int? targetAccountId, int budgetId, string userId)
        {
            try
            {
                //TODO: Return proper error code and message
                if (string.IsNullOrEmpty(templateName))
                {
                    return false;
                }
                if (!amount.HasValue && string.IsNullOrEmpty(description) && !categoryId.HasValue && !sourceAccountId.HasValue && !targetAccountId.HasValue)
                {
                    return false;
                }
                if (context.TransactionTemplates.Any(tt=>tt.UserId.Equals(userId) && tt.BudgetId.Equals(budgetId) && tt.Name.Equals(templateName)))
                {
                    return false;
                }

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

        public List<TransactionTemplateEntity> GetTransactionTemplates(int budgetId, string userId)
        {
            var list = context.TransactionTemplates.Where(tt => tt.IsActive && tt.BudgetId.Equals(budgetId) && tt.UserId.Equals(userId)).ToList();

            var entityList = new List<TransactionTemplateEntity>();
            list.ForEach(tt =>
            {
                entityList.Add(new TransactionTemplateEntity
                {
                    Id = tt.Id,
                    Name = tt.Name,
                    Amount = tt.Amount,
                    Description = tt.Description,
                    CategoryId = tt.CategoryId,
                    SourceAccountId = tt.SourceAccountId
                });
            });

            return entityList;
        }
    }
}
