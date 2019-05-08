namespace ExpenseTracker.Business.Context.DbModels
{
    public class AccountType : BaseEntity
    {
        public int AccountTypeId { get; set; }
        public string Name { get; set; }
    }
}
