using ExpenseTracker.Entities.Base;

namespace ExpenseTracker.Entities
{
    public class CurrencyEntity : BaseEntity
    {
        public int CurrencyId { get; set; }
        public string CurrencyCode { get; set; }
        public string LongName { get; set; }
        public string DisplayName { get; set; }
    }
}
