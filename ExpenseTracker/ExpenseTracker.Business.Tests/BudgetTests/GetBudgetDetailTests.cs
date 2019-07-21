using Xunit;

namespace ExpenseTracker.Business.Tests.BudgetTests
{
    public class GetBudgetDetailTests : BaseBudgetTest
    {
        [Fact]
        public void GetBudgetDetails_Success()
        {
            //TODO: Delete this and do it on initialize step if possible
            BaseTestInitialize();
            // ARRANGE
            var business = new BudgetBusiness(context);
            int budgetId = 1;
            string userId = "test";

            // ACT
            var budget = business.GetBudgetDetails(budgetId, userId);

            //ASSERT
            Assert.NotNull(budget);
            Assert.Equal(budgetId, budget.BudgetId);
            Assert.Equal("Budget_1", budget.Name);
            Assert.Equal("a", budget.InsertUser.Id);
        }

        [Fact]
        public void GetBudgetDetails_NotAuthorized()
        {
            //TODO: Delete this and do it on initialize step if possible
            BaseTestInitialize();
            // ARRANGE
            var business = new BudgetBusiness(context);
            int budgetId = 5;
            string userId = "testWildUser";

            // ACT
            var budget = business.GetBudgetDetails(budgetId, userId);

            //ASSERT
            Assert.Null(budget);
        }
    }
}
