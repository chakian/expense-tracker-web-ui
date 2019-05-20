using ExpenseTracker.Persistence.Context.DbModels;
using System.Collections.Generic;

namespace ExpenseTracker.WebUI.Models.BudgetRelated
{
    public class BudgetListModel : BaseModel
    {
        public List<BasicBudgetInfo> BudgetList { get; set; }
    }

    public class BasicBudgetInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CurrencyDisplayName { get; set; }
    }
}