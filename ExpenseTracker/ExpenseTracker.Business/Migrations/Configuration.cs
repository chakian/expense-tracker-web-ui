namespace ExpenseTracker.Business.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Context.ExpenseTrackerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ExpenseTracker.Business.Context.ExpenseTrackerContext";
        }

        protected override void Seed(Context.ExpenseTrackerContext context)
        {
            context.AccountTypes.AddOrUpdate(
                p => p.LookupID,
                new Context.DbModels.AccountType { LookupID = 1, LookupValue = "Bank Account", IsActive = true },
                new Context.DbModels.AccountType { LookupID = 2, LookupValue = "Savings Account", IsActive = true },
                new Context.DbModels.AccountType { LookupID = 3, LookupValue = "Credit Card", IsActive = true }
            );

            context.Currencies.AddOrUpdate(
                p=>p.LookupID,
                new Context.DbModels.Currency { LookupID = 1, LookupValue = "TL", IsActive = true },
                new Context.DbModels.Currency { LookupID = 2, LookupValue = "USD", IsActive = true },
                new Context.DbModels.Currency { LookupID = 3, LookupValue = "EUR", IsActive = true },
                new Context.DbModels.Currency { LookupID = 999, LookupValue = "Money", IsActive = true }
                //new Context.DbModels.Currency { LookupID = 5, LookupValue = "Bank Account", IsActive = true },
                //new Context.DbModels.Currency { LookupID = 6, LookupValue = "Bank Account", IsActive = true },
                //new Context.DbModels.Currency { LookupID = 7, LookupValue = "Bank Account", IsActive = true },
                //new Context.DbModels.Currency { LookupID = 8, LookupValue = "Bank Account", IsActive = true },
                //new Context.DbModels.Currency { LookupID = 9, LookupValue = "Bank Account", IsActive = true },
                //new Context.DbModels.Currency { LookupID = 10, LookupValue = "Bank Account", IsActive = true },
                //new Context.DbModels.Currency { LookupID = 11, LookupValue = "Bank Account", IsActive = true },
                //new Context.DbModels.Currency { LookupID = 12, LookupValue = "Bank Account", IsActive = true },
                //new Context.DbModels.Currency { LookupID = 13, LookupValue = "Bank Account", IsActive = true },
                //new Context.DbModels.Currency { LookupID = 14, LookupValue = "Bank Account", IsActive = true },
                //new Context.DbModels.Currency { LookupID = 15, LookupValue = "Bank Account", IsActive = true }
            );
        }
    }
}
