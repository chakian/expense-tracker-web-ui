using ExpenseTracker.Business.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Business.Context.DbModels
{
    public class BudgetUser : AuditableEntity
    {
        public int BudgetUserId { get; set; }

        public int BudgetId { get; set; }
        public virtual Budget Budget { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
