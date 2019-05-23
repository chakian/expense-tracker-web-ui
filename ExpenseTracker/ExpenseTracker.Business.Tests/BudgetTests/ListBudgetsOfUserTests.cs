using ExpenseTracker.Persistence.Context.DbModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ExpenseTracker.Business.Tests.BudgetTests
{
    [TestClass]
    public class ListBudgetsOfUserTests : BaseBudgetTest
    {
        [TestMethod]
        public void ListBudgetsOfUser_Success()
        {
            // ARRANGE
            var business = new BudgetBusiness(context);
            string userId = DefaultTestUserId;

            // ACT
            var budgets = business.GetBudgetsOfUser(userId);

            //ASSERT
            Assert.AreEqual(1, budgets.Count);
            Assert.AreEqual("Budget_1", budgets[0].Name);
        }

        [TestMethod]
        public void ListBudgetsOfUser_InactiveBudgetDoesntReturn()
        {
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
            Assert.AreEqual(1, budgets.Count);
            Assert.IsNull(budgets.FirstOrDefault(b => b.Name.Equals(nonExistingBudgetName)));
        }
    }
}
