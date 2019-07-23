﻿using ExpenseTracker.Persistence.Context.DbModels;
using ExpenseTracker.Persistence.Identity;
using Xunit;

namespace ExpenseTracker.Business.Tests.TransactionTemplateTests
{
    public class GetTransactionTemplateTests : BaseQueryTest
    {
        private readonly Budget DefaultBudget;
        private readonly AccountType DefaultAccountType;
        private readonly Account DefaultAccount;
        private readonly Category DefaultCategory;

        public GetTransactionTemplateTests()
        {
            // Create budget for user
            var budget = new BudgetBusiness(context).CreateBudget("testBudget", DefaultCurrency.CurrencyId, DefaultUserId);
            DefaultBudget = budget;

            // Create an account type
            var accountType = new AccountType { AccountTypeId = 1, Name = "Cash", IsActive = true };
            context.AccountTypes.Add(accountType);
            context.SaveChanges();
            DefaultAccountType = accountType;

            // Create account for user and budget
            var account = CreateNewAuthorizedEntity<Account>(DefaultUserId);
            account.BudgetId = DefaultBudget.BudgetId;
            account.CurrentBalance = account.StartingBalance = 5000;
            account.Name = "testAccount";
            account.AccountTypeId = DefaultAccountType.AccountTypeId;
            context.Accounts.Add(account);
            context.SaveChanges();
            DefaultAccount = account;

            // Create a category
            var category = CreateNewAuthorizedEntity<Category>(DefaultUserId);
            category.BudgetId = DefaultBudget.BudgetId;
            category.Name = "testCategory";
            context.Categories.Add(category);
            context.SaveChanges();
            DefaultCategory = category;
        }

        [Fact]
        public void GetTransactionTemplate_AllTemplatesOfBudgetForUser()
        {
            int? categoryId = DefaultCategory.CategoryId;
            int? sourceAccountId = DefaultAccount.AccountId;
            int? targetAccountId = null;

            // Arrange
            var business = new TransactionTemplateBusiness(context);
            business.CreateTransactionTemplate("New Template 1", 150, "Test Expense 1", categoryId, sourceAccountId, targetAccountId, DefaultBudget.BudgetId, DefaultUserId);
            business.CreateTransactionTemplate("New Template 2", 350, "Test Expense 2", categoryId, sourceAccountId, targetAccountId, DefaultBudget.BudgetId, DefaultUserId);

            // Act
            var actualList = business.GetTransactionTemplates(DefaultBudget.BudgetId, DefaultUserId);

            // Assert
            Assert.Equal(2, actualList.Count);
        }
    }
}
