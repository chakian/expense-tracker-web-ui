﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Persistence.Context.DbModels
{
    public class Category : AuditableEntity
    {
        public Category()
        {
            BudgetPlanCategories = new HashSet<BudgetPlanCategory>();
            Transactions = new HashSet<Transaction>();
        }

        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        [DefaultValue(false)]
        public bool IsIncomeCategory { get; set; }

        public int BudgetId { get; set; }
        public virtual Budget Budget { get; set; }

        public virtual ICollection<BudgetPlanCategory> BudgetPlanCategories { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
