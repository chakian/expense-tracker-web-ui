using System.Collections.Generic;

namespace ExpenseTracker.Business.Context.DbModels
{
    public class Category : AuditableEntity
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        #region Foreign Keys
        public int BudgetId { get; set; }
        public Budget Budget { get; set; }
        #endregion

        public virtual ICollection<BudgetPlanCategory> BudgetPlanCategories { get; set; }
    }
}
