using ExpenseTracker.Business.Identity;
using System;

namespace ExpenseTracker.Business.Context.DbModels
{
    public class AuditableEntity : BaseEntity
    {
        public string InsertUserId { get; set; }
        public User InsertUser { get; set; }
        public DateTime InsertTime { get; set; }

        public string UpdateUserId { get; set; }
        public User UpdateUser { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
