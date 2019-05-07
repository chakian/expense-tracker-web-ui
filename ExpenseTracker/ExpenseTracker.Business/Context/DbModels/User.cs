using System.Collections.Generic;

namespace ExpenseTracker.Business.Context.DbModels
{
    public class User : AuditableEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public virtual List<BudgetUser> BudgetUsers { get; set; }
    }
}
