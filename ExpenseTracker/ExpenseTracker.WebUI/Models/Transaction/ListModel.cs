using System.Web.Mvc;

namespace ExpenseTracker.WebUI.Models.Transaction
{
    public class ListModel : BaseTransactionModel
    {
        public int Month { get; set; }
        public int Year { get; set; }

        public string MonthName { get; set; }

        public int PreviousMonth { get; set; }
        public int PreviousYear { get; set; }
        public int NextMonth { get; set; }
        public int NextYear { get; set; }

        public int AccountId { get; set; }
        public SelectList AccountList { get; set; }

        public int CategoryId { get; set; }
        public SelectList CategoryList { get; set; }
    }
}