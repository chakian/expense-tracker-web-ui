using ExpenseTracker.Business.Context.DbModels;
using System.Data.Entity;

namespace ExpenseTracker.Business.Context.FluentConfiguration
{
    public class AccountConfiguration
    {
        public static void Configure(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(e => e.TransactionsBySourceAccount)
                .WithRequired(e => e.SourceAccount)
                .HasForeignKey(e => e.SourceAccountId);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.TransactionsByTargetAccount)
                .WithOptional(e => e.TargetAccount)
                .HasForeignKey(e => e.TargetAccountId);
        }
    }
}
