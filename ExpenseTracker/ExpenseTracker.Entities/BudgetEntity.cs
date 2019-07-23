using ExpenseTracker.Entities.Base;

namespace ExpenseTracker.Entities
{
    public class BudgetEntity : AuditableEntity
    {
        public int BudgetId { get; set; }
        public string Name { get; set; }
        public int CurrencyId { get; set; }
        public CurrencyEntity Currency { get; set; }
    }
}
