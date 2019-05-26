namespace ExpenseTracker.WebUI.Models.Transaction
{
    public class DeleteModel : BaseTransactionModel
    {
        public int TransactionId { get; set; }
        public string CategoryName { get; set; }
        public string AccountName { get; set; }
    }
}