using System.Collections.Generic;

namespace ExpenseTracker.Business.Context.DbModels
{
    public class BudgetPlan : AuditableEntity
    {
        public int Month { get; set; }
        public int Year { get; set; }

        #region Foreign Keys
        public int BudgetID { get; set; }
        #endregion

        public virtual List<BudgetPlanCategory> BudgetPlanCategories { get; set; }
    }
}
