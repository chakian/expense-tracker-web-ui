using ExpenseTracker.Business.Identity;
using System;

namespace ExpenseTracker.Business.Context.DbModels
{
    public class AuditableEntity : BaseEntity
    {
        public int InsertUserId { get; set; }
        public User InsertUser { get; set; }
        public DateTime InsertTime { get; set; }

        public int? UpdateUserId { get; set; }
        public User UpdateUser { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
