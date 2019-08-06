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

        [Fact]
        public void UpdateCategoryToHaveParent_Success()
        {
            // ARRANGE
            CategoryBusiness categoryBusiness = new CategoryBusiness(context);

            CategoryEntity categoryEntity = new CategoryEntity()
            {
                BudgetId = DefaultTestBudgetId,
                IsIncomeCategory = false,
                Name = "soon to have parent"
            };
            var initialInsert = categoryBusiness.CreateCategory(categoryEntity, DefaultUserId);
            Assert.NotNull(initialInsert);

            categoryEntity = new CategoryEntity()
            {
                BudgetId = DefaultTestBudgetId,
                IsIncomeCategory = false,
                Name = "This is parent"
            };
            CategoryEntity parent = categoryBusiness.CreateCategory(categoryEntity, DefaultUserId);
            Assert.NotNull(parent);

            // ACT
            initialInsert.ParentId = parent.CategoryId;
            CategoryEntity actual = categoryBusiness.UpdateCategory(initialInsert, DefaultUserId);

            // ASSERT
            Assert.Equal(initialInsert.CategoryId, actual.CategoryId);
            Assert.Equal(parent.CategoryId, actual.ParentId);
        }

        [Fact]
        public void UpdateCategoryToHaveParent_Fail_ParentDoesntExist()
        {
            // ARRANGE
            CategoryBusiness categoryBusiness = new CategoryBusiness(context);

            CategoryEntity categoryEntity = new CategoryEntity()
            {
                BudgetId = DefaultTestBudgetId,
                IsIncomeCategory = false,
                Name = "soon to have parent"
            };
            var initialInsert = categoryBusiness.CreateCategory(categoryEntity, DefaultUserId);
            Assert.NotNull(initialInsert);

            // ACT
            initialInsert.ParentId = -1;
            CategoryEntity actual = categoryBusiness.UpdateCategory(initialInsert, DefaultUserId);

            // ASSERT
            Assert.Null(actual);
        }

        [Fact]
        public void UpdateCategory_RemoveParent_Success()
        {
            // ARRANGE
            CategoryBusiness categoryBusiness = new CategoryBusiness(context);

            CategoryEntity categoryEntity = new CategoryEntity()
            {
                BudgetId = DefaultTestBudgetId,
                IsIncomeCategory = false,
                Name = "This is parent"
            };
            CategoryEntity parent = categoryBusiness.CreateCategory(categoryEntity, DefaultUserId);
            Assert.NotNull(parent);

            categoryEntity = new CategoryEntity()
            {
                BudgetId = DefaultTestBudgetId,
                IsIncomeCategory = false,
                Name = "child",
                ParentId = parent.CategoryId
            };
            var initialInsert = categoryBusiness.CreateCategory(categoryEntity, DefaultUserId);
            Assert.NotNull(initialInsert);

            // ACT
            initialInsert.ParentId = null;
            CategoryEntity actual = categoryBusiness.UpdateCategory(initialInsert, DefaultUserId);

            // ASSERT
            Assert.NotNull(actual);
            Assert.Null(actual.ParentId);
        }
    }
}
