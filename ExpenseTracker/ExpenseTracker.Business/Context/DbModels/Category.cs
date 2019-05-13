using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Business.Context.DbModels
{
    public class Category : AuditableEntity
    {
        public Category()
        {
            BudgetPlanCategories = new HashSet<BudgetPlanCategory>();
            Transactions = new HashSet<Transaction>();
        }

        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        public int BudgetId { get; set; }
        public virtual Budget Budget { get; set; }

        public virtual ICollection<BudgetPlanCategory> BudgetPlanCategories { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
