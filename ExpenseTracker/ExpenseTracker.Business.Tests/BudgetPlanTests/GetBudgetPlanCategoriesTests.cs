using ExpenseTracker.Business.Tests.Base;
using ExpenseTracker.Entities;
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

        [Fact]
        public void Success_NoRecordedPlan()
        {
            // ARRANGE
            var business = new BudgetPlanBusiness(context);
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
            BudgetPlanEntity budgetPlan = business.GetBudgetPlanById(budgetPlanId, userId);

            //ASSERT
            Assert.NotNull(budgetPlan);
            Assert.Equal(categoryNames.Count, budgetPlan.BudgetPlanCategories.Select(q => q.Category.Name).ToList().Count);
            foreach (BudgetPlanCategoryEntity item in budgetPlan.BudgetPlanCategories)
            {
                Assert.NotNull(item.Category);
            }
        }
    }
}
