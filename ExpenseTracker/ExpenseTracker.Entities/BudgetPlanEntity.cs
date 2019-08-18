using System.Collections.Generic;

namespace ExpenseTracker.Entities
{
    public class BudgetPlanEntity
    {
        public int BudgetPlanId { get; set; }

        public int Month { get; set; }
        public int Year { get; set; }

        public IList<BudgetPlanCategoryEntity> BudgetPlanCategories { get; set; }
    }
}
