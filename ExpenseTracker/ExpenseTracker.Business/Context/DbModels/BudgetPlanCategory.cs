using System.Collections.Generic;

namespace ExpenseTracker.Business.Context.DbModels
{
    public class BudgetPlanCategory : AuditableEntity
    {
        public decimal PlannedAmount { get; set; }

        #region Foreign Keys
        public int BudgetPlanID { get; set; }
        public int CategoryID { get; set; }
        #endregion

        public BudgetPlan BudgetPlan { get; set; }
        public Category Category { get; set; }
    }
}
