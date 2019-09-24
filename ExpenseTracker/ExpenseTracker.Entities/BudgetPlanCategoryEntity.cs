namespace ExpenseTracker.Entities
{
    public class BudgetPlanCategoryEntity
    {
        public int BudgetPlanCategoryId { get; set; }

        public decimal PlannedAmount { get; set; }

        public int CategoryId { get; set; }
        public virtual CategoryEntity Category { get; set; }
    }
}
