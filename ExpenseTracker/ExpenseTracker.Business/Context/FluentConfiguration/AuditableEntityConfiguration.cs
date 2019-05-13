using ExpenseTracker.Business.Context.DbModels;
using System.Data.Entity;

namespace ExpenseTracker.Business.Context.FluentConfiguration
{
    public class AuditableEntityConfiguration
    {
        public static void Configure(DbModelBuilder modelBuilder)
        {
            //#region budget plans
            //modelBuilder.Entity<User>()
            //    .HasMany(e => e.BudgetPlans_InsertUser)
            //    .WithRequired(e => e.InsertUser)
            //    .HasForeignKey(e => e.InsertUserId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<User>()
            //    .HasMany(e => e.BudgetPlans_UpdateUser)
            //    .WithOptional(e => e.UpdateUser)
            //    .HasForeignKey(e => e.UpdateUserId);

            //modelBuilder.Entity<BudgetPlan>()
            //    .HasRequired(s => s.InsertUser)
            //    .WithMany()
            //    .HasForeignKey(e => e.InsertUserId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<BudgetPlan>()
            //    .HasOptional(s => s.UpdateUser)
            //    .WithMany()
            //    .HasForeignKey(e => e.UpdateUserId);
            //#endregion
            Configure<Account>(modelBuilder);
            Configure<Budget>(modelBuilder);
            Configure<BudgetPlan>(modelBuilder);
            Configure<BudgetPlanCategory>(modelBuilder);
            Configure<BudgetUser>(modelBuilder);
            Configure<Category>(modelBuilder);
            Configure<Transaction>(modelBuilder);

            //#region budget plan category
            //modelBuilder.Entity<User>()
            //    .HasMany(e => e.BudgetPlanCategories_InsertUser)
            //    .WithRequired(e => e.InsertUser)
            //    .HasForeignKey(e => e.InsertUserId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<User>()
            //    .HasMany(e => e.BudgetPlanCategories_UpdateUser)
            //    .WithOptional(e => e.UpdateUser)
            //    .HasForeignKey(e => e.UpdateUserId);
            //#endregion

            //#region budget
            //modelBuilder.Entity<User>()
            //    .HasMany(e => e.Budgets_InsertUser)
            //    .WithRequired(e => e.InsertUser)
            //    .HasForeignKey(e => e.InsertUserId);

            //modelBuilder.Entity<User>()
            //    .HasMany(e => e.Budgets_UpdateUser)
            //    .WithOptional(e => e.UpdateUser)
            //    .HasForeignKey(e => e.UpdateUserId);
            //#endregion

            //#region account
            //modelBuilder.Entity<User>()
            //    .HasMany(e => e.Accounts_InsertUser)
            //    .WithRequired(e => e.InsertUser)
            //    .HasForeignKey(e => e.InsertUserId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<User>()
            //    .HasMany(e => e.Accounts_UpdateUser)
            //    .WithOptional(e => e.UpdateUser)
            //    .HasForeignKey(e => e.UpdateUserId);
            //#endregion

            //#region budget user
            //modelBuilder.Entity<User>()
            //    .HasMany(e => e.BudgetUsers_InsertUser)
            //    .WithRequired(e => e.InsertUser)
            //    .HasForeignKey(e => e.InsertUserId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<User>()
            //    .HasMany(e => e.BudgetUsers_UpdateUser)
            //    .WithOptional(e => e.UpdateUser)
            //    .HasForeignKey(e => e.UpdateUserId);
            //#endregion

            //#region category
            //modelBuilder.Entity<User>()
            //    .HasMany(e => e.Categories_InsertUser)
            //    .WithRequired(e => e.InsertUser)
            //    .HasForeignKey(e => e.InsertUserId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<User>()
            //    .HasMany(e => e.Categories_UpdateUser)
            //    .WithOptional(e => e.UpdateUser)
            //    .HasForeignKey(e => e.UpdateUserId);
            //#endregion

            //#region transaction
            //modelBuilder.Entity<User>()
            //    .HasMany(e => e.Transactions_InsertUser)
            //    .WithRequired(e => e.InsertUser)
            //    .HasForeignKey(e => e.InsertUserId);

            //modelBuilder.Entity<User>()
            //    .HasMany(e => e.Transactions_UpdateUser)
            //    .WithOptional(e => e.UpdateUser)
            //    .HasForeignKey(e => e.UpdateUserId);
            //#endregion
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
