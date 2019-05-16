using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpenseTracker.Persistence.Context;
using Dbo = ExpenseTracker.Persistence.Context.DbModels;

namespace ExpenseTracker.Business.Tests.Budget
{
    /// <summary>
    /// Summary description for BudgetTests
    /// </summary>
    [TestClass]
    public class BudgetTests : BaseQueryTest
    {
        [TestInitialize()]
        public void MyTestInitialize()
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

        [TestMethod]
        public void ListBudgetsOfUser_Success()
        {
            // ARRANGE
            var business = new BudgetBusiness(context);
            string userId = "test";

            // ACT
            var budgets = business.GetBudgetsOfUser(userId);

            //ASSERT
            Assert.AreEqual(3, budgets.Count);
            Assert.AreEqual("Budget_1", budgets[0].Name);
            Assert.AreEqual("Budget_2", budgets[1].Name);
            Assert.AreEqual("Budget_3", budgets[2].Name);
        }
    }
}
