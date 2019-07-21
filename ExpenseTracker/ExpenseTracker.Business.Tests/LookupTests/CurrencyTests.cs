using ExpenseTracker.Persistence.Context;
using Xunit;

namespace ExpenseTracker.Business.Tests.LookupTests
{
    public class CurrencyTests : BaseQueryTest
    {
        //[TestInitialize()]
        private void CurrencyTestInitialize()
        {
            var connection = Effort.DbConnectionFactory.CreateTransient();
            context = new ExpenseTrackerContext(connection);

            CreateDefaultCurrencies();
        }

        [Fact]
        public void GetCurrencyList_Success()
        {
            CurrencyTestInitialize();
            // ARRANGE
            var business = new CurrencyBusiness(context);

            // ACT
            var currencyList = business.GetCurrencyList();

            //ASSERT
            Assert.NotNull(currencyList);
            Assert.Equal(3, currencyList.Count);
        }
    }
}
