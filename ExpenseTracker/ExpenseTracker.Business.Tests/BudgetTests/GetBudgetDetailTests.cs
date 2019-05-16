using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpenseTracker.Business.Tests.BudgetTests
{
    [TestClass]
    public class GetBudgetDetailTests : BaseBudgetTest
    {
        [TestMethod]
        public void GetBudgetDetails_Success()
        {
            // ARRANGE
            var business = new BudgetBusiness(context);
            int budgetId = 1;
            string userId = "test";

            // ACT
            var budget = business.GetBudgetDetails(budgetId, userId);

            //ASSERT
            Assert.IsNotNull(budget);
            Assert.AreEqual(budgetId, budget.BudgetId);
            Assert.AreEqual("Budget_1", budget.Name);
            Assert.AreEqual("a", budget.InsertUser.Id);
        }

        [TestMethod]
        public void GetBudgetDetails_NotAuthorized()
        {
            // ARRANGE
            var business = new BudgetBusiness(context);
            int budgetId = 5;
            string userId = "testWildUser";

            // ACT
            var budget = business.GetBudgetDetails(budgetId, userId);

            //ASSERT
            Assert.IsNull(budget);
        }
    }
}
