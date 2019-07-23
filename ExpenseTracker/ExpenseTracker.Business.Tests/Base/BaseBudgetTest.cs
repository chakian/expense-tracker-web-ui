using ExpenseTracker.Persistence.Context;
using Dbo = ExpenseTracker.Persistence.Context.DbModels;
using System.Collections.Generic;

namespace ExpenseTracker.Business.Tests.Base
{
    public class BaseBudgetTest : BaseTest
    {
        protected int DefaultTestBudgetId = -1;

        public BaseBudgetTest()
        {
            if (context == null)
            {
                var connection = Effort.DbConnectionFactory.CreateTransient();
                context = new ExpenseTrackerContext(connection);
            }

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
            budgetUser.UserId = DefaultUserId;
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
