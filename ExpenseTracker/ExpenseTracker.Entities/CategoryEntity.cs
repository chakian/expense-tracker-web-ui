using ExpenseTracker.Entities.Base;

namespace ExpenseTracker.Entities
{
    public class CategoryEntity : AuditableEntity
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public bool IsIncomeCategory { get; set; }
        public int BudgetId { get; set; }
        public BudgetEntity Budget { get; set; }
    }
}
