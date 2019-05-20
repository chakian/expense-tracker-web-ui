using System;

namespace ExpenseTracker.WebUI.Models.ContextObjects
{
    public class Expense : BaseModel
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int AccountID { get; set; }
    }
}