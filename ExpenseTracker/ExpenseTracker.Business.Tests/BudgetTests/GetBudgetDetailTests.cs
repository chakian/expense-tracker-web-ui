using ExpenseTracker.Business.Tests.Base;
using ExpenseTracker.Entities;
using Xunit;

namespace ExpenseTracker.Business.Tests.BudgetTests
{
    public class GetBudgetDetailTests : BaseBudgetTest
    {
        [Fact]
        public void GetBudgetDetails_Success()
        {
            // ARRANGE
            var business = new BudgetBusiness(context);
            int budgetId = 1;
            string userId = DefaultUserId;

            // ACT
            BudgetEntity budget = business.GetBudgetDetails(budgetId, userId);

            //ASSERT
            Assert.NotNull(budget);
            Assert.Equal(budgetId, budget.BudgetId);
            Assert.Equal("Budget_1", budget.Name);
            Assert.Equal(DefaultUserId, budget.InsertUser.Id);
        }

        [Fact]
        public void GetBudgetDetails_NotAuthorized()
        {
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
