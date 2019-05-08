using System.Collections.Generic;

namespace ExpenseTracker.Business.Context.DbModels
{
    public class BudgetPlan : AuditableEntity
    {
        public int BudgetPlanId { get; set; }

        public int Month { get; set; }
        public int Year { get; set; }

        #region Foreign Keys
        public int BudgetId { get; set; }
        public Budget Budget { get; set; }
        #endregion

        public virtual ICollection<BudgetPlanCategory> BudgetPlanCategories { get; set; }
    }
}
