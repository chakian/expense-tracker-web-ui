namespace ExpenseTracker.Persistence.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Context.ExpenseTrackerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ExpenseTracker.Persistence.Context.ExpenseTrackerContext";
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
                p => p.CurrencyId,
                new Context.DbModels.Currency { CurrencyId = 1, CurrencyCode = "TRY", DisplayName = "TL", LongName="T�rk Liras�", IsActive = true },
                new Context.DbModels.Currency { CurrencyId = 2, CurrencyCode = "USD", DisplayName = "USD", LongName = "Amerikan Dolar�", IsActive = true },
                new Context.DbModels.Currency { CurrencyId = 3, CurrencyCode = "EUR", DisplayName = "EUR", LongName = "Euro", IsActive = true },
                new Context.DbModels.Currency { CurrencyId = 999, CurrencyCode = "MONEY", DisplayName = "Para", LongName = "Para", IsActive = true }
                //new Context.DbModels.Currency { CurrencyId = 5, CurrencyCode = "CUR", DisplayName = "Para", LongName = "Para Birimi", IsActive = true },
            );
        }
    }
}
