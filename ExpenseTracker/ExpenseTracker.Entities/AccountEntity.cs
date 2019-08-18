namespace ExpenseTracker.Entities
{
    public class AccountEntity
    {
        public int AccountId { get; set; }
        public string Name { get; set; }
        public decimal StartingBalance { get; set; }
        public decimal CurrentBalance { get; set; }

        public int AccountTypeId { get; set; }
        public AccountTypeEntity AccountType { get; set; }

        public int BudgetId { get; set; }
    }
}
