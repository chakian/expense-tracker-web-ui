using ExpenseTracker.Business.Context.DbModels;
using ExpenseTracker.Business.Context.FluentConfiguration;
using System.Data.Entity;

namespace ExpenseTracker.Business.Context
{
    public class ExpenseTrackerContext : DbContext
    {
        public ExpenseTrackerContext()
            : base("name=ExpenseTrackerContext")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountType> AccountTypes { get; set; }
        public virtual DbSet<BudgetPlanCategory> BudgetPlanCategories { get; set; }
        public virtual DbSet<BudgetPlan> BudgetPlans { get; set; }
        public virtual DbSet<Budget> Budgets { get; set; }
        public virtual DbSet<BudgetUser> BudgetUsers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            AuditableEntityConfiguration.Configure(modelBuilder);

            AccountConfiguration.Configure(modelBuilder);

            BudgetConfiguration.Configure(modelBuilder);

            CategoryConfiguration.Configure(modelBuilder);

            UserConfiguration.Configure(modelBuilder);
        }
    }
}
