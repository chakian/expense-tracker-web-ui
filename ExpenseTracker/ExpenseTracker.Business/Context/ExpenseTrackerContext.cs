using ExpenseTracker.Business.Context.DbModels;
using System.Data.Entity;

namespace ExpenseTracker.Business.Context
{
    public class ExpenseTrackerContext : DbContext
    {
        public ExpenseTrackerContext() : base("name=ExpenseTrackerContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<BudgetUser> BudgetUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<BudgetPlan> BudgetPlans { get; set; }
        public DbSet<BudgetPlanCategory> BudgetPlanCategories { get; set; }
    }
}
