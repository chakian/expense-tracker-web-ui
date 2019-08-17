using ExpenseTracker.Business.Tests.Base;
using System;
using Xunit;

namespace ExpenseTracker.Business.Tests.BudgetPlanTests
{
    public class GetBudgetPlanTests : BaseBudgetPlanTest
    {
        private readonly string UNAUTHORIZED_USER = "notauthorized";

        [Fact]
        public void GetBudgetPlanById_Success()
        {
            // ARRANGE
            var business = new BudgetPlanBusiness(context);
            string userId = DefaultUserId;
            int budgetPlanId = CreateBudgetPlan(DefaultTestBudgetId, 2019, 05, userId);

            //// ACT
            //var budgetPlan = business.GetBudgetPlanById(budgetPlanId, userId);

            ////ASSERT
            //Assert.NotNull(budgetPlan);
        }

        [Fact]
        public void GetBudgetPlanForCurrentDate_Success_CurrentDatePlanDoesntExist()
        {
            // ARRANGE
            var business = new BudgetPlanBusiness(context);
            int budgetId = DefaultTestBudgetId;
            string userId = DefaultUserId;
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;

            //// ACT
            //var budgetPlan = business.GetBudgetPlanByYearAndMonth(budgetId, year, month, userId);

            ////ASSERT
            //Assert.NotNull(budgetPlan);
        }

        [Fact]
        public void GetBudgetPlanForCurrentDate_Success_CurrentDatePlanExists()
        {
            // ARRANGE
            var business = new BudgetPlanBusiness(context);
            int budgetId = DefaultTestBudgetId;
            string userId = DefaultUserId;
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;

            CreateBudgetPlan(budgetId, year, month, userId);

            //// ACT
            //var budgetPlan = business.GetBudgetPlanByYearAndMonth(budgetId, year, month, userId);

            ////ASSERT
            //Assert.NotNull(budgetPlan);
        }

        [Fact]
        public void GetBudgetPlanForDate_Success_DateIsNextMonth()
        {
            // ARRANGE
            var business = new BudgetPlanBusiness(context);
            int budgetId = DefaultTestBudgetId;
            string userId = DefaultUserId;

            DateTime currentDate = DateTime.Now;
            CreateBudgetPlan(budgetId, currentDate.Year, currentDate.Month, userId);

            DateTime adjacentDate = currentDate.AddMonths(1);

            //// ACT
            //var budgetPlan = business.GetBudgetPlanByYearAndMonth(budgetId, adjacentDate.Year, adjacentDate.Month, userId);

            ////ASSERT
            //Assert.NotNull(budgetPlan);
        }

        [Fact]
        public void GetBudgetPlanForDate_Success_DateIsPreviousMonth()
        {
            // ARRANGE
            var business = new BudgetPlanBusiness(context);
            int budgetId = DefaultTestBudgetId;
            string userId = DefaultUserId;

            DateTime currentDate = DateTime.Now;
            CreateBudgetPlan(budgetId, currentDate.Year, currentDate.Month, userId);

            DateTime adjacentDate = currentDate.AddMonths(-1);

            //// ACT
            //var budgetPlan = business.GetBudgetPlanByYearAndMonth(budgetId, adjacentDate.Year, adjacentDate.Month, userId);

            ////ASSERT
            //Assert.NotNull(budgetPlan);
        }

        [Fact]
        public void GetBudgetPlanForDate_Fail_DateIsNotAdjacent()
        {
            // ARRANGE
            var business = new BudgetPlanBusiness(context);
            int budgetId = DefaultTestBudgetId;
            string userId = DefaultUserId;

            DateTime currentDate = DateTime.Now;
            CreateBudgetPlan(budgetId, currentDate.Year, currentDate.Month, userId);

            DateTime notAdjacentDate = currentDate.AddMonths(2);

            //// ACT
            //var budgetPlan = business.GetBudgetPlanByYearAndMonth(budgetId, notAdjacentDate.Year, notAdjacentDate.Month, userId);

            ////ASSERT
            //Assert.Null(budgetPlan);
        }

        [Fact]
        public void GetBudgetPlanForDate_Fail_UserIsNotAuthorizedForBudget()
        {
            // ARRANGE
            var business = new BudgetPlanBusiness(context);
            int budgetId = DefaultTestBudgetId;
            string unauthorizedUserId = UNAUTHORIZED_USER;

            DateTime currentDate = DateTime.Now;
            CreateBudgetPlan(budgetId, currentDate.Year, currentDate.Month, DefaultUserId);

            DateTime notAdjacentDate = currentDate.AddMonths(2);

            //// ACT
            //var budgetPlan = business.GetBudgetPlanByYearAndMonth(budgetId, notAdjacentDate.Year, notAdjacentDate.Month, unauthorizedUserId);

            ////ASSERT
            //Assert.Null(budgetPlan);
        }
    }
}
