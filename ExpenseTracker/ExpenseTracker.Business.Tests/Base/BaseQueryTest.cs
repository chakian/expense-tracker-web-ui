using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Context.DbModels;
using ExpenseTracker.Persistence.Identity;
using System;
using System.Collections.Generic;

namespace ExpenseTracker.Business.Tests
{
    public class BaseQueryTest
    {
        protected ExpenseTrackerContext context;

        public BaseQueryTest()
        {
            var connection = Effort.DbConnectionFactory.CreateTransient();
            context = new ExpenseTrackerContext(connection);
        }

        //TODO: Changed for backwards compatibility. Delete it when refactoring.
        protected T CreateNewAuthorizedEntity<T>()
            where T : AuditableEntity, new()
        {
            T obj = CreateNewAuthorizedEntity<T>("a");
            return obj;
        }
        protected T CreateNewAuthorizedEntity<T>(string userId)
            where T : AuditableEntity, new()
        {
            T obj = new T();
            obj.InsertUserId = userId;
            obj.InsertTime = DateTime.Now;
            obj.IsActive = true;
            return obj;
        }

        protected void CreateDefaultCurrencies()
        {
            var currencyList = new List<Currency>();
            var currency = new Currency { IsActive = true, CurrencyId = 1, CurrencyCode = "TRY", DisplayName = "TL", LongName = "Türk Lirası" };
            currencyList.Add(currency);

            currency = new Currency { IsActive = true, CurrencyId = 2, CurrencyCode = "USD", DisplayName = "USD", LongName = "Dolar" };
            currencyList.Add(currency);

            currency = new Currency { IsActive = true, CurrencyId = 3, CurrencyCode = "EUR", DisplayName = "EUR", LongName = "Euro" };
            currencyList.Add(currency);

            context.Currencies.AddRange(currencyList);
            context.SaveChanges();
        }

        protected void CreateDefaultUsers(string defaultTestUserId)
        {
            var user = new User { IsActive = true, UserName = "A", Id = "a", Email = "a@a.a" };
            context.Users.Add(user);

            user = new User { IsActive = true, UserName = "B", Id = "b", Email = "b@b.b" };
            context.Users.Add(user);

            user = new User { IsActive = true, UserName = "TEST", Id = defaultTestUserId, Email = "test@test.test" };
            context.Users.Add(user);

            context.SaveChanges();
        }
    }
}
