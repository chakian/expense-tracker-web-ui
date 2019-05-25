using ExpenseTracker.Business.Tests.Base;
using ExpenseTracker.Persistence.Context.DbModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ExpenseTracker.Business.Tests.BudgetPlanTests
{
    [TestClass]
    public class GetBudgetPlanTests : BaseBudgetPlanTest
    {
        private readonly string UNAUTHORIZED_USER = "notauthorized";

        private int CreateBudgetPlan(int budgetId, int year, int month, string userId)
        {
            BudgetPlan currentDatePlan = CreateNewAuthorizedEntity<BudgetPlan>();
            currentDatePlan.BudgetId = budgetId;
            currentDatePlan.Year = year;
            currentDatePlan.Month = month;
            context.BudgetPlans.Add(currentDatePlan);
            context.SaveChanges();

            return currentDatePlan.BudgetPlanId;
        }

        [TestMethod]
        public void GetBudgetPlanById_Fail_Null()
        {
            // ARRANGE
            var business = new BudgetPlanBusiness(context);
            string userId = DefaultTestUserId;
            int budgetPlanId = 0;

            // ACT
            var budgetPlan = business.GetBudgetPlanById(budgetPlanId, userId);

            //ASSERT
            Assert.IsNull(budgetPlan);
        }

        [TestMethod]
        public void GetBudgetPlanById_Success()
        {
            // ARRANGE
            var business = new BudgetPlanBusiness(context);
            string userId = DefaultTestUserId;
            int budgetPlanId = CreateBudgetPlan(DefaultTestBudgetId, 2019, 05, userId);

            // ACT
            var budgetPlan = business.GetBudgetPlanById(budgetPlanId, userId);

            //ASSERT
            Assert.IsNotNull(budgetPlan);
        }

        [TestMethod]
        public void GetBudgetPlanForCurrentDate_Success_CurrentDatePlanDoesntExist()
        {
            // ARRANGE
            var business = new BudgetPlanBusiness(context);
            int budgetId = DefaultTestBudgetId;
            string userId = DefaultTestUserId;
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;

            // ACT
            var budgetPlan = business.GetBudgetPlanByYearAndMonth(budgetId, year, month, userId);

            //ASSERT
            Assert.IsNotNull(budgetPlan);
        }

        [TestMethod]
        public void GetBudgetPlanForCurrentDate_Success_CurrentDatePlanExists()
        {
            // ARRANGE
            var business = new BudgetPlanBusiness(context);
            int budgetId = DefaultTestBudgetId;
            string userId = DefaultTestUserId;
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;

            CreateBudgetPlan(budgetId, year, month, userId);

            // ACT
            var budgetPlan = business.GetBudgetPlanByYearAndMonth(budgetId, year, month, userId);

            //ASSERT
            Assert.IsNotNull(budgetPlan);
        }

        [TestMethod]
        public void GetBudgetPlanForDate_Success_DateIsNextMonth()
        {
            // ARRANGE
            var business = new BudgetPlanBusiness(context);
            int budgetId = DefaultTestBudgetId;
            string userId = DefaultTestUserId;

            DateTime currentDate = DateTime.Now;
            CreateBudgetPlan(budgetId, currentDate.Year, currentDate.Month, userId);

            DateTime adjacentDate = currentDate.AddMonths(1);

            // ACT
            var budgetPlan = business.GetBudgetPlanByYearAndMonth(budgetId, adjacentDate.Year, adjacentDate.Month, userId);

            //ASSERT
            Assert.IsNotNull(budgetPlan);
        }

        [TestMethod]
        public void GetBudgetPlanForDate_Success_DateIsPreviousMonth()
        {
            // ARRANGE
            var business = new BudgetPlanBusiness(context);
            int budgetId = DefaultTestBudgetId;
            string userId = DefaultTestUserId;

            DateTime currentDate = DateTime.Now;
            CreateBudgetPlan(budgetId, currentDate.Year, currentDate.Month, userId);

            DateTime adjacentDate = currentDate.AddMonths(-1);

            // ACT
            var budgetPlan = business.GetBudgetPlanByYearAndMonth(budgetId, adjacentDate.Year, adjacentDate.Month, userId);

            //ASSERT
            Assert.IsNotNull(budgetPlan);
        }

        [TestMethod]
        public void GetBudgetPlanForDate_Fail_DateIsNotAdjacent()
        {
            // ARRANGE
            var business = new BudgetPlanBusiness(context);
            int budgetId = DefaultTestBudgetId;
            string userId = DefaultTestUserId;

            DateTime currentDate = DateTime.Now;
            CreateBudgetPlan(budgetId, currentDate.Year, currentDate.Month, userId);

            DateTime notAdjacentDate = currentDate.AddMonths(2);

            // ACT
            var budgetPlan = business.GetBudgetPlanByYearAndMonth(budgetId, notAdjacentDate.Year, notAdjacentDate.Month, userId);

            //ASSERT
            Assert.IsNull(budgetPlan);
        }

        [TestMethod]
        public void GetBudgetPlanForDate_Fail_UserIsNotAuthorizedForBudget()
        {
            // ARRANGE
            var business = new BudgetPlanBusiness(context);
            int budgetId = DefaultTestBudgetId;
            string userId = UNAUTHORIZED_USER;

            DateTime currentDate = DateTime.Now;
            CreateBudgetPlan(budgetId, currentDate.Year, currentDate.Month, userId);

            DateTime notAdjacentDate = currentDate.AddMonths(2);

            // ACT
            var budgetPlan = business.GetBudgetPlanByYearAndMonth(budgetId, notAdjacentDate.Year, notAdjacentDate.Month, userId);

            //ASSERT
            Assert.IsNull(budgetPlan);
        }
    }
}
