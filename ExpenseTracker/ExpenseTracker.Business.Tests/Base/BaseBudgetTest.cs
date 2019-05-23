using ExpenseTracker.Persistence.Context;
using Dbo = ExpenseTracker.Persistence.Context.DbModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ExpenseTracker.Business.Tests
{
    public class BaseBudgetTest : BaseQueryTest
    {
        protected int DefaultTestBudgetId = -1;
        protected const string DefaultTestUserId = "test";

        [TestInitialize()]
        public void BaseTestInitialize()
        {
            if (context == null)
            {
                var connection = Effort.DbConnectionFactory.CreateTransient();
                context = new ExpenseTrackerContext(connection);
            }

            CreateDefaultCurrencies();
            CreateDefaultUsers(DefaultTestUserId);

            CreateDefaultBudgetsAndAssignUsers();
        }

        private void CreateDefaultBudgetsAndAssignUsers()
        {
            CreateDefaultBudgets();
            AssignDefaultUserToDefaultBudget();
        }

        protected void AssignDefaultUserToDefaultBudget()
        {
            var budgetUserData = new List<Dbo.BudgetUser>();
            var budgetUser = CreateNewAuthorizedEntity<Dbo.BudgetUser>();
            budgetUser.BudgetId = DefaultTestBudgetId;
            budgetUser.UserId = DefaultTestUserId;
            budgetUserData.Add(budgetUser);

            context.BudgetUsers.AddRange(budgetUserData);
            context.SaveChanges();
        }

        protected void CreateDefaultBudgets()
        {
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

            DefaultTestBudgetId = budgetData[0].BudgetId;
        }
    }
}
