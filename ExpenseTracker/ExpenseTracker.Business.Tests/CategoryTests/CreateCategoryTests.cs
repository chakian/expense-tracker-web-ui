using ExpenseTracker.Business.Tests.Base;
using ExpenseTracker.Entities;
using Xunit;

namespace ExpenseTracker.Business.Tests.CategoryTests
{
    public class CreateCategoryTests : BaseBudgetTest
    {
        [Fact]
        public void CreateFirstCategory_Expense_Success()
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

            // ACT
            CategoryEntity actual = categoryBusiness.CreateCategory(categoryEntity, DefaultUserId);

            // ASSERT
            Assert.Equal(expectedCategoryName, actual.Name);
            Assert.False(actual.IsIncomeCategory);
            Assert.NotEqual(0, actual.CategoryId);
            Assert.NotEqual(0, actual.BudgetId);
        }

        [Fact]
        public void CreateFirstCategory_Income_Success()
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

            // ACT
            CategoryEntity actual = categoryBusiness.CreateCategory(categoryEntity, DefaultUserId);

            // ASSERT
            Assert.Equal(expectedCategoryName, actual.Name);
            Assert.True(actual.IsIncomeCategory);
            Assert.NotEqual(0, actual.CategoryId);
            Assert.NotEqual(0, actual.BudgetId);
        }

        [Fact]
        public void CreateCategory_Fail_SameNameActiveCategory()
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
            categoryBusiness.CreateCategory(categoryEntity, DefaultUserId);

            // ACT
            CategoryEntity actual = categoryBusiness.CreateCategory(categoryEntity, DefaultUserId);

            // ASSERT
            Assert.Null(actual);
        }
    }
}
