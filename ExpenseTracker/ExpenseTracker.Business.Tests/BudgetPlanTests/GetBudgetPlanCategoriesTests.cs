using ExpenseTracker.Business.Tests.Base;
using ExpenseTracker.Persistence.Context.DbModels;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ExpenseTracker.Business.Tests.BudgetPlanTests
{
    public class GetBudgetPlanCategoriesTests : BaseBudgetPlanTest
    {
        private Category CreateCategory(string name, int budgetId)
        {
            Category category = CreateNewAuthorizedEntity<Category>();
            category.BudgetId = budgetId;
            category.Name = name;
            var added = context.Categories.Add(category);
            context.SaveChanges();

            return added;
        }
        private void CreatePlanCategory(int categoryId, int budgetPlanId, decimal amount)
        {
            BudgetPlanCategory category = CreateNewAuthorizedEntity<BudgetPlanCategory>();
            category.BudgetPlanId = budgetPlanId;
            category.CategoryId = categoryId;
            category.PlannedAmount = amount;
            context.BudgetPlanCategories.Add(category);
            context.SaveChanges();
        }

        [Fact]
        public void GetBudgetPlanCategories_Success_NoRecordedPlan()
        {
            // ARRANGE
            var business = new BudgetPlanCategoryBusiness(context);
            string userId = DefaultUserId;
            int budgetPlanId = CreateBudgetPlan(DefaultTestBudgetId, 2019, 05, userId);

            List<string> categoryNames = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                string categoryName = "category" + i.ToString();
                CreateCategory(categoryName, DefaultTestBudgetId);
                categoryNames.Add(categoryName);
            }

            // ACT
            var budgetPlanCategories = business.GetBudgetPlanCategoriesByPlanId(budgetPlanId, userId);

            //ASSERT
            Assert.NotNull(budgetPlanCategories);
            Assert.Equal(categoryNames.Count, budgetPlanCategories.Select(q => q.Category.Name).ToList().Count);
        }

        [Fact]
        public void GetBudgetPlanCategories_Success_SomeRecordedPlan()
        {
            // ARRANGE
            var business = new BudgetPlanCategoryBusiness(context);
            string userId = DefaultUserId;
            int budgetPlanId = CreateBudgetPlan(DefaultTestBudgetId, 2019, 05, userId);

            List<string> categoryNames = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                string categoryName = "category" + i.ToString();
                var addedCategory = CreateCategory(categoryName, DefaultTestBudgetId);
                categoryNames.Add(categoryName);

                if (i % 2 == 0)
                {
                    CreatePlanCategory(addedCategory.CategoryId, budgetPlanId, 10 * (i + 1));
                }
            }

            // ACT
            var budgetPlanCategories = business.GetBudgetPlanCategoriesByPlanId(budgetPlanId, userId);

            //ASSERT
            Assert.NotNull(budgetPlanCategories);
            Assert.Equal(categoryNames.Count, budgetPlanCategories.Select(q => q.Category.Name).ToList().Count);
            Assert.Equal(10, budgetPlanCategories.Single(q => q.Category.Name.Equals("category0")).PlannedAmount);
            Assert.Equal(0, budgetPlanCategories.Single(q => q.Category.Name.Equals("category1")).PlannedAmount);
            Assert.Equal(30, budgetPlanCategories.Single(q => q.Category.Name.Equals("category2")).PlannedAmount);
            Assert.Equal(0, budgetPlanCategories.Single(q => q.Category.Name.Equals("category3")).PlannedAmount);
            Assert.Equal(50, budgetPlanCategories.Single(q => q.Category.Name.Equals("category4")).PlannedAmount);
        }
    }
}
