using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Business.Context.DbModels
{
    public class AuditableEntity : BaseEntity
    {
        [ForeignKey("InsertUser")]
        public int InsertUserId { get; set; }
        public User InsertUser { get; set; }
        public DateTime InsertTime { get; set; }

        [ForeignKey("UpdateUser")]
        public int? UpdateUserId { get; set; }
        public User UpdateUser { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
