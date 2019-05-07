namespace ExpenseTracker.Business.Context.DbModels
{
    public class BudgetUser : AuditableEntity
    {
        #region Foreign Keys
        public int BudgetID { get; set; }
        public int UserID { get; set; }
        #endregion

        public Budget Budget { get; set; }
        public User User { get; set; }
    }
}
