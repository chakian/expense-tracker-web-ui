using ExpenseTracker.Persistence.Context.DbModels;

namespace ExpenseTracker.Business.Tests.Base
{
    public class BaseBudgetPlanTest : BaseBudgetTest
    {
        //TODO: [TestInitialize()]
        public new void BaseTestInitialize()
        {
            base.BaseTestInitialize();
        }

        protected int CreateBudgetPlan(int budgetId, int year, int month, string userId)
        {
            BudgetPlan currentDatePlan = CreateNewAuthorizedEntity<BudgetPlan>();
            currentDatePlan.BudgetId = budgetId;
            currentDatePlan.Year = year;
            currentDatePlan.Month = month;
            context.BudgetPlans.Add(currentDatePlan);
            context.SaveChanges();

            return currentDatePlan.BudgetPlanId;
        }
    }
}
