using ExpenseTracker.Persistence.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpenseTracker.Business.Tests.LookupTests
{
    [TestClass]
    public class CurrencyTests : BaseQueryTest
    {
        [TestInitialize()]
        public void CurrencyTestInitialize()
        {
            var connection = Effort.DbConnectionFactory.CreateTransient();
            context = new ExpenseTrackerContext(connection);

            CreateDefaultCurrencies();
        }

        [TestMethod]
        public void GetCurrencyList_Success()
        {
            // ARRANGE
            var business = new CurrencyBusiness(context);

            // ACT
            var currencyList = business.GetCurrencyList();

            //ASSERT
            Assert.IsNotNull(currencyList);
            Assert.AreEqual(3, currencyList.Count);
        }
    }
}
