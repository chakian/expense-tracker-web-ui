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
                p => p.AccountTypeId,
                new Context.DbModels.AccountType { AccountTypeId = 1, Name = "Bank Account", IsActive = true },
                new Context.DbModels.AccountType { AccountTypeId = 2, Name = "Savings Account", IsActive = true },
                new Context.DbModels.AccountType { AccountTypeId = 3, Name = "Credit Card", IsActive = true }
            );

            context.Currencies.AddOrUpdate(
                p=>p.CurrencyId,
                new Context.DbModels.Currency { CurrencyId = 1, Name = "TL", IsActive = true },
                new Context.DbModels.Currency { CurrencyId = 2, Name = "USD", IsActive = true },
                new Context.DbModels.Currency { CurrencyId = 3, Name = "EUR", IsActive = true },
                new Context.DbModels.Currency { CurrencyId = 999, Name = "Money", IsActive = true }
                //new Context.DbModels.Currency { CurrencyId = 5, LookupValue = "Bank Account", IsActive = true },
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
