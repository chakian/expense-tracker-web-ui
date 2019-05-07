using System;

namespace ExpenseTracker.Business.Context.DbModels
{
    public class Transaction : AuditableEntity
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

        #region Foreign Keys
        public int? CategoryID { get; set; }
        public int SourceAccountID { get; set; }
        public int? TargetAccountID { get; set; }
        #endregion

        public Category Category { get; set; }
        public Account SourceAccount { get; set; }
        public Account TargetAccount { get; set; }
    }
}
