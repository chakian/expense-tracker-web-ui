using System.Collections.Generic;

namespace ExpenseTracker.Business.Context.DbModels
{
    public class Account : AuditableEntity
    {
        public string Name { get; set; }
        public decimal StartingBalance { get; set; }
        public decimal CurrentBalance { get; set; }

        #region Foreign Keys
        public int AccountTypeID { get; set; }
        public int CurrencyID { get; set; }
        public int BudgetID { get; set; }
        #endregion

        public AccountType AccountType { get; set; }
        public Currency Currency { get; set; }
        public Budget Budget { get; set; }
    }
}
