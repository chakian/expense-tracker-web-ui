namespace ExpenseTracker.WebUI.Models.ContextObjects
{
    public class BudgetPlan
    {
        public int BudgetPlanId { get; set; }

        public int BudgetId { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public string MonthName { get; set; }
    }
}