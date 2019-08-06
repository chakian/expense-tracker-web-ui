using ExpenseTracker.Business.Tests.Base;
using ExpenseTracker.Entities;
using Xunit;

namespace ExpenseTracker.Business.Tests.CategoryTests
{
    public class UpdateCategoryTests : BaseBudgetTest
    {
        [Fact]
        public void UpdateIncomeCategoryToExpense_Success()
        {
            // ARRANGE
            CategoryBusiness categoryBusiness = new CategoryBusiness(context);

            string expectedCategoryName = "My First Category";
            CategoryEntity categoryEntity = new CategoryEntity()
            {
                BudgetId = DefaultTestBudgetId,
                IsIncomeCategory = true,
                Name = expectedCategoryName
            };
            var initialInsert = categoryBusiness.CreateCategory(categoryEntity, DefaultUserId);
            Assert.NotNull(initialInsert);

            // ACT
            initialInsert.IsIncomeCategory = false;
            CategoryEntity actual = categoryBusiness.UpdateCategory(initialInsert, DefaultUserId);

            // ASSERT
            Assert.Equal(expectedCategoryName, actual.Name);
            Assert.False(actual.IsIncomeCategory);
        }

        [Fact]
        public void UpdateExpenseCategoryToIncome_Success()
        {
            // ARRANGE
            CategoryBusiness categoryBusiness = new CategoryBusiness(context);

            string expectedCategoryName = "My First Category";
            CategoryEntity categoryEntity = new CategoryEntity()
            {
                BudgetId = DefaultTestBudgetId,
                IsIncomeCategory = false,
                Name = expectedCategoryName
            };
            var initialInsert = categoryBusiness.CreateCategory(categoryEntity, DefaultUserId);
            Assert.NotNull(initialInsert);

            // ACT
            initialInsert.IsIncomeCategory = true;
            CategoryEntity actual = categoryBusiness.UpdateCategory(initialInsert, DefaultUserId);

            // ASSERT
            Assert.Equal(expectedCategoryName, actual.Name);
            Assert.True(actual.IsIncomeCategory);
        }
    }
}
