//using ExpenseTracker.Persistence.Context.DbModels;
//using System.Collections.Generic;
//using Xunit;

//namespace ExpenseTracker.Business.Tests.TransactionDraftTests
//{
//    public class CreateTransactionDraftTests : BaseQueryTest
//    {
//        private readonly Currency DefaultCurrency;
//        public CreateTransactionDraftTests()
//        {
//            // Create default currency
//            var currencyList = new List<Currency>();
//            var currency = new Currency { IsActive = true, CurrencyId = 1, CurrencyCode = "TRY", DisplayName = "TL", LongName = "Türk Lirası" };
//            currencyList.Add(currency);
//            context.Currencies.AddRange(currencyList);
//            context.SaveChanges();
//            DefaultCurrency = currency;
//        }

//        [Theory]
//        [InlineData(null, null, null, 1)]
//        [InlineData(null, null, 2, null)]
//        [InlineData(null, null, 2, 1)]
//        [InlineData(null, "sigara", null, null)]
//        [InlineData(null, "sigara", null, 1)]
//        [InlineData(null, "sigara", 2, null)]
//        [InlineData(null, "sigara", 2, 1)]
//        [InlineData(15, null, null, null)]
//        [InlineData(15, null, null, 1)]
//        [InlineData(15, null, 2, null)]
//        [InlineData(15, null, 2, 1)]
//        [InlineData(15, "sigara", null, null)]
//        [InlineData(15, "sigara", null, 1)]
//        [InlineData(15, "sigara", 2, null)]
//        [InlineData(15, "sigara", 2, 1)]
//        public void CreateTransactionDraft_Success(double? _amount, string description, int? categoryId, int? sourceAccountId)
//        {
//            //"decimal?" type was not an option, so I had to use "double?"
//            //decimal? amount = (decimal?)_amount;

//            //// Arrange
//            //var business = new TransactionDraftBusiness(context);

//            //// Act
//            //var actualResult = business.CreateTransactionDraft(amount, description, categoryId, sourceAccountId, budgetId, userId);

//            //// Assert
//            //Assert.NotNull(actualResult);
//        }

//        [Fact]
//        public void CreateTransactionDraft_Fail()
//        {
//        }
//    }
//}
