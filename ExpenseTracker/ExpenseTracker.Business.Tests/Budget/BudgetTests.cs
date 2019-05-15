using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpenseTracker.Persistence.Context;
using Dbo = ExpenseTracker.Persistence.Context.DbModels;
using System.Linq;
using Moq;

namespace ExpenseTracker.Business.Tests.Budget
{
    /// <summary>
    /// Summary description for BudgetTests
    /// </summary>
    [TestClass]
    public class BudgetTests : BaseQueryTest
    {
        private Mock<ExpenseTrackerContext> context;

        [TestInitialize()]
        public void MyTestInitialize()
        {
            context = new Mock<ExpenseTrackerContext>();

            var budgetData = new List<Dbo.Budget>
            {
                new Dbo.Budget { BudgetId = 1, Name = "BBB", IsActive=true },
                new Dbo.Budget { BudgetId = 2, Name = "ZZZ", IsActive=true },
                new Dbo.Budget { BudgetId = 3, Name = "AAA", IsActive=true },
                new Dbo.Budget { BudgetId = 4, Name = "CCC", IsActive=true },
                new Dbo.Budget { BudgetId = 5, Name = "DDD", IsActive=false },
                new Dbo.Budget { BudgetId = 6, Name = "EEE", IsActive=true },
            }.AsQueryable();
            context.Setup(c => c.Budgets).Returns(PrepareMockSet(budgetData).Object);
        }

        [TestMethod]
        public void ListBudgetsOfUser_Success()
        {
            // ARRANGE
            var business = new BudgetBusiness(context.Object);
            string userId = "testUser";

            // ACT
            var budgets = business.GetBudgetsOfUser(userId);

            //ASSERT
            Assert.AreEqual(3, budgets.Count);
            Assert.AreEqual("BBB", budgets[0].Name);
            Assert.AreEqual("AAA", budgets[1].Name);
            Assert.AreEqual("CCC", budgets[2].Name);
        }
    }
}
