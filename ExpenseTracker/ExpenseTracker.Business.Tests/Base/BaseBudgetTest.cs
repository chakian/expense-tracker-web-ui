using ExpenseTracker.Persistence.Context;
using Dbo = ExpenseTracker.Persistence.Context.DbModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ExpenseTracker.Business.Tests
{
    public class BaseBudgetTest : BaseQueryTest
    {
        [TestInitialize()]
        public void BaseBudgetTestInitialize()
        {
            var connection = Effort.DbConnectionFactory.CreateTransient();
            context = new ExpenseTrackerContext(connection);

            CreateDefaultCurrencies();
            CreateDefaultUsers();

            var budgetData = new List<Dbo.Budget>();
            for (int i = 1; i <= 10; i++)
            {
                Dbo.Budget budget = CreateNewAuthorizedEntity<Dbo.Budget>();
                budget.Name = "Budget_" + i.ToString();
                budget.CurrencyId = 1;
                budgetData.Add(budget);
            }
            context.Budgets.AddRange(budgetData);
            context.SaveChanges();

            var budgetUserData = new List<Dbo.BudgetUser>();
            var budgetUser = CreateNewAuthorizedEntity<Dbo.BudgetUser>();
            budgetUser.BudgetId = 1;
            budgetUser.UserId = "test";
            budgetUserData.Add(budgetUser);

            budgetUser = CreateNewAuthorizedEntity<Dbo.BudgetUser>();
            budgetUser.BudgetId = 2;
            budgetUser.UserId = "test";
            budgetUserData.Add(budgetUser);

            budgetUser = CreateNewAuthorizedEntity<Dbo.BudgetUser>();
            budgetUser.BudgetId = 3;
            budgetUser.UserId = "test";
            budgetUserData.Add(budgetUser);

            context.BudgetUsers.AddRange(budgetUserData);
            context.SaveChanges();
        }
    }
}
