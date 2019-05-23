using ExpenseTracker.WebUI.Models.ContextObjects;
using System.Collections.Generic;

namespace ExpenseTracker.WebUI.Models.BudgetRelated
{
    public class BudgetPlanUpdateModel : BaseModel
    {
        public BudgetPlan BudgetPlan { get; set; }

        public List<BudgetPlanCategory> Categories { get; set; }
    }
}