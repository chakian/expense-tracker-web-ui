using System;

namespace ExpenseTracker.Business.Context.DbModels
{
    public class Expense
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
