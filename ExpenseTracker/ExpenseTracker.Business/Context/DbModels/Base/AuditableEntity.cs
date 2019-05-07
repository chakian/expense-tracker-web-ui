using System;

namespace ExpenseTracker.Business.Context.DbModels
{
    public class AuditableEntity : BaseEntity
    {
        public int InsertUserID { get; set; }
        public DateTime InsertTime { get; set; }
        public int UpdateUserID { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
