using System;

namespace ExpenseTracker.WebUI.Models.Expense
{
    public class AddModel : BaseModel
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int AccountID { get; set; }
    }
}
