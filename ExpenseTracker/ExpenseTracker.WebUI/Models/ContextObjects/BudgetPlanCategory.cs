namespace ExpenseTracker.WebUI.Models.ContextObjects
{
    public class BudgetPlanCategory
    {
        public int BudgetPlanCategoryId { get; set; }

        public decimal PlannedAmount { get; set; }

        public decimal SpentAmount { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}