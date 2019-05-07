using System;

namespace ExpenseTracker.Business.Context.DbModels
{
    public class Transaction : AuditableEntity
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

        #region Foreign Keys
        public int CategoryID { get; set; }
        #endregion

        public Category Category { get; set; }
    }
}
