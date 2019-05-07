using System.Collections.Generic;

namespace ExpenseTracker.Business.Context.DbModels
{
    public class Budget : AuditableEntity
    {
        public string Name { get; set; }

        #region Foreign Keys
        public int CurrencyID { get; set; }
        #endregion

        public Currency Currency { get; set; }

        public virtual List<BudgetUser> BudgetUsers { get; set; }
        public virtual List<Category> Categories { get; set; }
        public virtual List<Account> Accounts { get; set; }
    }
}
