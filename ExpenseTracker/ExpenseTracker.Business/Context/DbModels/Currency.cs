namespace ExpenseTracker.Business.Context.DbModels
{
    public class Currency : BaseEntity
    {
        public int CurrencyId { get; set; }
        public string Name { get; set; }
    }
}
