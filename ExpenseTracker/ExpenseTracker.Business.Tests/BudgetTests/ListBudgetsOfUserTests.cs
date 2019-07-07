using ExpenseTracker.Persistence.Context.DbModels;
using System.Linq;
using Xunit;

namespace ExpenseTracker.Business.Tests.BudgetTests
{
    public class ListBudgetsOfUserTests : BaseBudgetTest
    {
        [Fact]
        public void ListBudgetsOfUser_Success()
        {
            //TODO: Delete this and do it on initialize step if possible
            BaseTestInitialize();
            // ARRANGE
            var business = new BudgetBusiness(context);
            string userId = DefaultTestUserId;

            // ACT
            var budgets = business.GetBudgetsOfUser(userId);

            //ASSERT
            Assert.Equal(1, budgets.Count);
            Assert.Equal("Budget_1", budgets[0].Name);
        }

        [Fact]
        public void ListBudgetsOfUser_InactiveBudgetDoesntReturn()
        {
            //TODO: Delete this and do it on initialize step if possible
            BaseTestInitialize();
            // ARRANGE
            var business = new BudgetBusiness(context);
            string userId = "test";

            string nonExistingBudgetName = "Budget_99";

            var budget = CreateNewAuthorizedEntity<Budget>();
            budget.IsActive = false;
            budget.Name = nonExistingBudgetName;
            budget.CurrencyId = 1;
            context.Budgets.Add(budget);
            var budgetUser = CreateNewAuthorizedEntity<BudgetUser>();
            budgetUser.BudgetId = 11;
            budgetUser.UserId = DefaultTestUserId;
            context.BudgetUsers.Add(budgetUser);
            context.SaveChanges();

            // ACT
            var budgets = business.GetBudgetsOfUser(userId);

            //ASSERT
            Assert.Equal(1, budgets.Count);
            Assert.Null(budgets.FirstOrDefault(b => b.Name.Equals(nonExistingBudgetName)));
        }
    }
}
