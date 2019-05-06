using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ExpenseTracker.WebUI.Models
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

        public System.Data.Entity.DbSet<ExpenseTracker.WebUI.Models.ContextObjects.Expense> Expenses { get; set; }
    }
}
