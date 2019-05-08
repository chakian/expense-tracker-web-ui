using System.Collections.Generic;

namespace ExpenseTracker.Business.Context.DbModels
{
    public class Budget : AuditableEntity
    {
        public int BudgetId { get; set; }

        public string Name { get; set; }

        #region Foreign Keys
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        #endregion

        public virtual ICollection<BudgetUser> BudgetUsers { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
