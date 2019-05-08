namespace ExpenseTracker.Business.Context.DbModels
{
    public class Account : AuditableEntity
    {
        public int AccountId { get; set; }

        public string Name { get; set; }
        public decimal StartingBalance { get; set; }
        public decimal CurrentBalance { get; set; }

        #region Foreign Keys
        public int AccountTypeId { get; set; }
        public AccountType AccountType { get; set; }

        public int BudgetId { get; set; }
        public Budget Budget { get; set; }
        #endregion
    }
}
