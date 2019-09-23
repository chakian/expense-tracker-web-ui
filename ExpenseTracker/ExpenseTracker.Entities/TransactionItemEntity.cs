using System;

namespace ExpenseTracker.Entities
{
    public class TransactionItemEntity
    {
        public int TransactionItemId { get; set; }
        public int TransactionId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

        public int CategoryId { get; set; }
        public CategoryEntity Category { get; set; }
    }
}
