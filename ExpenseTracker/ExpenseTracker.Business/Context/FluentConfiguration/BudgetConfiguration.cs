using ExpenseTracker.Business.Context.DbModels;
using System.Data.Entity;

namespace ExpenseTracker.Business.Context.FluentConfiguration
{
    public class BudgetConfiguration
    {
        public static void Configure(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Budget>()
                .HasMany(e => e.Accounts)
                .WithRequired(e => e.Budget)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Budget>()
                .HasMany(e => e.Categories)
                .WithRequired(e => e.Budget)
                .WillCascadeOnDelete(false);
        }
    }
}
