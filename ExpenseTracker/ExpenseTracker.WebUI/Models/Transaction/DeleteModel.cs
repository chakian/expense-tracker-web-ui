namespace ExpenseTracker.WebUI.Models.Transaction
{
    public class DeleteModel : BaseEditableTransactionModel
    {
        public int TransactionId { get; set; }
        public string CategoryName { get; set; }
        public string AccountName { get; set; }
    }
}