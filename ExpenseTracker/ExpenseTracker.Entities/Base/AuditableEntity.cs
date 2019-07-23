using System;

namespace ExpenseTracker.Entities.Base
{
    public class AuditableEntity : BaseEntity
    {
        public string InsertUserId { get; set; }
        public UserEntity InsertUser { get; set; }
        public DateTime InsertTime { get; set; }

        public string UpdateUserId { get; set; }
        public UserEntity UpdateUser { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
