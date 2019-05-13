//using ExpenseTracker.Business.Context.DbModels;
//using System.Data.Entity;

//namespace ExpenseTracker.Business.Context.FluentConfiguration
//{
//    public class UserConfiguration
//    {
//        public static void Configure(DbModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<User>()
//                .HasOptional(s => s.InsertUser)
//                .WithMany()
//                .HasForeignKey(e => e.InsertUserId);

//            modelBuilder.Entity<User>()
//                .HasOptional(s => s.UpdateUser)
//                .WithMany()
//                .HasForeignKey(e => e.UpdateUserId);
//        }
//    }
//}
