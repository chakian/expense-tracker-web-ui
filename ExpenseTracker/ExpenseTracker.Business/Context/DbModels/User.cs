using System;
using System.Collections.Generic;

namespace ExpenseTracker.Business.Context.DbModels
{
    public class User : BaseEntity
    {
        public User()
        {
            BudgetUsers_Users = new HashSet<BudgetUser>();
            InsertedByUser = new HashSet<User>();
            UpdatedByUser = new HashSet<User>();

            //#region insert/update user
            //Accounts_InsertUser = new HashSet<Account>();
            //Accounts_UpdateUser = new HashSet<Account>();
            //BudgetPlanCategories_InsertUser = new HashSet<BudgetPlanCategory>();
            //BudgetPlanCategories_UpdateUser = new HashSet<BudgetPlanCategory>();
            //BudgetPlans_InsertUser = new HashSet<BudgetPlan>();
            //BudgetPlans_UpdateUser = new HashSet<BudgetPlan>();
            //Budgets_InsertUser = new HashSet<Budget>();
            //Budgets_UpdateUser = new HashSet<Budget>();
            //BudgetUsers_InsertUser = new HashSet<BudgetUser>();
            //BudgetUsers_UpdateUser = new HashSet<BudgetUser>();
            //Categories_InsertUser = new HashSet<Category>();
            //Categories_UpdateUser = new HashSet<Category>();
            //Transactions_InsertUser = new HashSet<Transaction>();
            //Transactions_UpdateUser = new HashSet<Transaction>();
            //#endregion
        }

        public int UserId { get; set; }

        public int? InsertUserId { get; set; }
        public virtual User InsertUser { get; set; }

        public DateTime InsertTime { get; set; }

        public int? UpdateUserId { get; set; }
        public virtual User UpdateUser { get; set; }

        public DateTime? UpdateTime { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public virtual ICollection<BudgetUser> BudgetUsers_Users { get; set; }

        public virtual ICollection<User> InsertedByUser { get; set; }

        public virtual ICollection<User> UpdatedByUser { get; set; }

        //#region insert/update user
        //public virtual ICollection<Account> Accounts_InsertUser { get; set; }
        //public virtual ICollection<Account> Accounts_UpdateUser { get; set; }

        //public virtual ICollection<BudgetPlanCategory> BudgetPlanCategories_InsertUser { get; set; }
        //public virtual ICollection<BudgetPlanCategory> BudgetPlanCategories_UpdateUser { get; set; }

        //public virtual ICollection<BudgetPlan> BudgetPlans_InsertUser { get; set; }
        //public virtual ICollection<BudgetPlan> BudgetPlans_UpdateUser { get; set; }

        //public virtual ICollection<Budget> Budgets_InsertUser { get; set; }
        //public virtual ICollection<Budget> Budgets_UpdateUser { get; set; }

        //public virtual ICollection<BudgetUser> BudgetUsers_InsertUser { get; set; }
        //public virtual ICollection<BudgetUser> BudgetUsers_UpdateUser { get; set; }

        //public virtual ICollection<Category> Categories_InsertUser { get; set; }
        //public virtual ICollection<Category> Categories_UpdateUser { get; set; }

        //public virtual ICollection<Transaction> Transactions_InsertUser { get; set; }
        //public virtual ICollection<Transaction> Transactions_UpdateUser { get; set; }
        //#endregion
    }
}
