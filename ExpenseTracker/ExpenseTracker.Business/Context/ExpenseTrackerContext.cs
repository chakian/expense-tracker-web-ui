using ExpenseTracker.Business.Context.DbModels;
using System.Data.Entity;

namespace ExpenseTracker.Business.Context
{
    public class ExpenseTrackerContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        //TODO: Code First
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ExpenseTrackerContext() : base("name=ExpenseTrackerContext")
        {
        }

        public DbSet<Expense> Expenses { get; set; }
    }
}
