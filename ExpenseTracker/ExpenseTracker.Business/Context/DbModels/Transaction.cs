using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Business.Context.DbModels
{
    public class Transaction : AuditableEntity
    {
        public int TransactionId { get; set; }

        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

        #region Foreign Keys
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        [ForeignKey("SourceAccount")]
        public int SourceAccountId { get; set; }
        public Account SourceAccount { get; set; }

        [ForeignKey("TargetAccount")]
        public int? TargetAccountId { get; set; }
        public Account TargetAccount { get; set; }
        #endregion
    }
}
