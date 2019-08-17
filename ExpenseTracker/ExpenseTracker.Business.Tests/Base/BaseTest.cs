using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Context.DbModels;
using ExpenseTracker.Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace ExpenseTracker.Business.Tests.Base
{
    public class BaseTest
    {
        protected ExpenseTrackerContext context;

        private readonly User DefaultUser;
        protected readonly Currency DefaultCurrency;

        protected string DefaultUserId { get { return DefaultUser?.Id; } }

        public BaseTest(string defaultTestUserId = "defaultTestUserId")
        {
            DbConnection connection = Effort.DbConnectionFactory.CreateTransient();
            context = new ExpenseTrackerContext(connection);

            List<Currency> currencies = CreateDefaultCurrencies();
            DefaultCurrency = currencies[0];
            DefaultUser = CreateDefaultUser(defaultTestUserId);
        }

        protected T CreateNewAuthorizedEntity<T>(string userId = null)
            where T : AuditableDbo, new()
        {
            if (string.IsNullOrEmpty(userId)) userId = DefaultUser.Id;

            T obj = new T
            {
                InsertUserId = userId,
                InsertTime = DateTime.Now,
                IsActive = true
            };
            return obj;
        }

        private List<Currency> CreateDefaultCurrencies()
        {
            List<Currency> currencyList = new List<Currency>();
            Currency currency = new Currency { IsActive = true, CurrencyId = 1, CurrencyCode = "TRY", DisplayName = "TL", LongName = "Türk Lirası" };
            currencyList.Add(currency);

            currency = new Currency { IsActive = true, CurrencyId = 2, CurrencyCode = "USD", DisplayName = "USD", LongName = "Dolar" };
            currencyList.Add(currency);

            currency = new Currency { IsActive = true, CurrencyId = 3, CurrencyCode = "EUR", DisplayName = "EUR", LongName = "Euro" };
            currencyList.Add(currency);

            context.Currencies.AddRange(currencyList);
            context.SaveChanges();

            return currencyList;
        }

        private User CreateDefaultUser(string defaultTestUserId)
        {
            User user = new User { IsActive = true, UserName = "TEST", Id = defaultTestUserId, Email = "test@test.test" };
            context.Users.Add(user);

            context.SaveChanges();

            return user;
        }
    }
}
