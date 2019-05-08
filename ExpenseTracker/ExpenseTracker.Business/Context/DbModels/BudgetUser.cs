namespace ExpenseTracker.Business.Context.DbModels
{
    public class BudgetUser : AuditableEntity
    {
        public int BudgetUserId { get; set; }

        #region Foreign Keys
        public int BudgetId { get; set; }
        public Budget Budget { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
        #endregion
    }
}
