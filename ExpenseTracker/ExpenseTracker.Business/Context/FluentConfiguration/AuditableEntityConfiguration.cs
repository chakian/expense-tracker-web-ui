using ExpenseTracker.Business.Context.DbModels;
using System.Data.Entity;

namespace ExpenseTracker.Business.Context.FluentConfiguration
{
    public class AuditableEntityConfiguration
    {
        public static void Configure(DbModelBuilder modelBuilder)
        {
            Configure<Account>(modelBuilder);
            Configure<Budget>(modelBuilder);
            Configure<BudgetPlan>(modelBuilder);
            Configure<BudgetPlanCategory>(modelBuilder);
            Configure<BudgetUser>(modelBuilder);
            Configure<Category>(modelBuilder);
            Configure<Transaction>(modelBuilder);
        }

        private static void Configure<T>(DbModelBuilder modelBuilder)
            where T : AuditableEntity
        {
            modelBuilder.Entity<T>()
                .HasRequired(s => s.InsertUser)
                .WithMany()
                .HasForeignKey(e => e.InsertUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T>()
                .HasOptional(s => s.UpdateUser)
                .WithMany()
                .HasForeignKey(e => e.UpdateUserId);
        }
    }
}
