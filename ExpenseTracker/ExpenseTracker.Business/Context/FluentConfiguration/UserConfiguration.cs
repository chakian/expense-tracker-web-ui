using ExpenseTracker.Business.Context.DbModels;
using System.Data.Entity;

namespace ExpenseTracker.Business.Context.FluentConfiguration
{
    public class UserConfiguration
    {
        public static void Configure(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(e => e.InsertedByUser)
                .WithOptional(e => e.InsertUser)
                .HasForeignKey(e => e.InsertUserId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UpdatedByUser)
                .WithOptional(e => e.UpdateUser)
                .HasForeignKey(e => e.UpdateUserId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.BudgetUsers_Users)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}
