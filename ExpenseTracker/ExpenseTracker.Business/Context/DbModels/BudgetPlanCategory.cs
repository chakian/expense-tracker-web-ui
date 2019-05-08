using System.Collections.Generic;

namespace ExpenseTracker.Business.Context.DbModels
{
    public class BudgetPlanCategory : AuditableEntity
    {
        public int BudgetPlanCategoryId { get; set; }

        public decimal PlannedAmount { get; set; }

        #region Foreign Keys
        public int BudgetPlanId { get; set; }
        public BudgetPlan BudgetPlan { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        #endregion
    }
}
