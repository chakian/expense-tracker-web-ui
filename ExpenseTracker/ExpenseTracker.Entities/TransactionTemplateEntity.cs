namespace ExpenseTracker.Entities
{
    public class TransactionTemplateEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Amount { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public int? SourceAccountId { get; set; }
    }
}
