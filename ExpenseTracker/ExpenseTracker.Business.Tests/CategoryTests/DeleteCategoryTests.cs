using ExpenseTracker.Business.Tests.Base;
using ExpenseTracker.Entities;
using System.Linq;
using Xunit;

namespace ExpenseTracker.Business.Tests.CategoryTests
{
    public class DeleteCategoryTests : BaseBudgetTest
    {
        [Fact]
        public void DeleteCategory_Success()
        {
            // ARRANGE
            CategoryBusiness categoryBusiness = new CategoryBusiness(context);

            string categoryName = "My First Category";
            CategoryEntity categoryEntity = new CategoryEntity()
            {
                BudgetId = DefaultTestBudgetId,
                Name = categoryName
            };
            var inserted = categoryBusiness.CreateCategory(categoryEntity, DefaultUserId);
            Assert.NotNull(inserted);

            // ACT
            bool isDeleted = categoryBusiness.DeleteCategory(inserted.CategoryId, DefaultUserId);
            Assert.True(isDeleted);

            var actual = categoryBusiness.GetCategoriesByBudgetId(DefaultTestBudgetId, DefaultUserId).SingleOrDefault(c=>c.Name.Equals(categoryName));

            // ASSERT
            Assert.Null(actual);
        }

        [Fact]
        public void DeleteCategory_Fail_HasChildCategories()
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
                Name = "child",
                ParentId = parent.CategoryId
            };
            var child = categoryBusiness.CreateCategory(categoryEntity, DefaultUserId);
            Assert.NotNull(child);

            // ACT
            bool isDeleted = categoryBusiness.DeleteCategory(parent.CategoryId, DefaultUserId);

            // ASSERT
            Assert.False(isDeleted);
        }
    }
}
