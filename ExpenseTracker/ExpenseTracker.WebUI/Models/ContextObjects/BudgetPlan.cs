namespace ExpenseTracker.WebUI.Models.ContextObjects
{
    public class BudgetPlan
    {
        public int BudgetPlanId { get; set; }

        public int BudgetId { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public string MonthName { get; set; }

        public int PreviousMonth { get; set; }
        public int PreviousYear { get; set; }
        public int NextMonth { get; set; }
        public int NextYear { get; set; }
    }
}