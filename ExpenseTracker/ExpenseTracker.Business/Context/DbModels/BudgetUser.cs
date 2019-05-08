using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Business.Context.DbModels
{
    public class BudgetUser : AuditableEntity
    {
        public int BudgetUserId { get; set; }

        #region Foreign Keys
        [ForeignKey("Budget")]
        public int BudgetId { get; set; }
        public Budget Budget { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        #endregion
    }
}
