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
            // ARRANGE
            var business = new BudgetBusiness(context);
            string userId = DefaultUserId;

            // ACT
            var budgets = business.GetBudgetsOfUser(userId);

            //ASSERT
            Assert.NotEmpty(budgets);
            Assert.Single(budgets);
            Assert.Equal("Budget_1", budgets[0].Name);
        }

        [Fact]
        public void ListBudgetsOfUser_InactiveBudgetDoesntReturn()
        {
            // ARRANGE
            var business = new BudgetBusiness(context);
            string userId = DefaultUserId;

            string nonExistingBudgetName = "Budget_99";

            var budget = CreateNewAuthorizedEntity<Budget>();
            budget.IsActive = false;
            budget.Name = nonExistingBudgetName;
            budget.CurrencyId = 1;
            context.Budgets.Add(budget);
            var budgetUser = CreateNewAuthorizedEntity<BudgetUser>();
            budgetUser.BudgetId = 11;
            budgetUser.UserId = DefaultUserId;
            context.BudgetUsers.Add(budgetUser);
            context.SaveChanges();

            // ACT
            var budgets = business.GetBudgetsOfUser(userId);

            //ASSERT
            Assert.Single(budgets);
            Assert.Null(budgets.FirstOrDefault(b => b.Name.Equals(nonExistingBudgetName)));
        }
    }
}
