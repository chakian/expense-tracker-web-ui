using ExpenseTracker.Business.Tests.Base;
using ExpenseTracker.Persistence.Context;
using Xunit;

namespace ExpenseTracker.Business.Tests.LookupTests
{
    public class CurrencyTests : BaseTest
    {
        [Fact]
        public void GetCurrencyList_Success()
        {
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
