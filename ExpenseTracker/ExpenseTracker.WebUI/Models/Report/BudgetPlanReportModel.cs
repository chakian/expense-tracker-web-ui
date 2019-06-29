using ExpenseTracker.WebUI.Models.ContextObjects;
using System.Collections.Generic;

namespace ExpenseTracker.WebUI.Models.Report
{
    public class BudgetPlanReportModel : BaseModel
    {
        public BudgetPlan BudgetPlan { get; set; }

        public List<BudgetPlanCategory> PlanCategories { get; set; }
    }
}
