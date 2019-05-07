using System.Collections.Generic;

namespace ExpenseTracker.Business.Context.DbModels
{
    public class Account : AuditableEntity
    {
        public string Name { get; set; }
        public decimal StartingBalance { get; set; }
        public decimal CurrentBalance { get; set; }

        #region Foreign Keys
        public int BudgetID { get; set; }
        #endregion

        public Budget Budget { get; set; }
    }
}
