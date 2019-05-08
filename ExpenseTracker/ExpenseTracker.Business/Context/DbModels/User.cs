using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Business.Context.DbModels
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        [ForeignKey("InsertUser")]
        public int? InsertUserId { get; set; }
        public User InsertUser { get; set; }
        public DateTime InsertTime { get; set; }

        [ForeignKey("UpdateUser")]
        public int? UpdateUserId { get; set; }
        public User UpdateUser { get; set; }
        public DateTime? UpdateTime { get; set; }

        public virtual ICollection<BudgetUser> BudgetUsers { get; set; }
    }
}
