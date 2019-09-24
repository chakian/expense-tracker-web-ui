//using ExpenseTracker.Business.Tests.Base;
//using ExpenseTracker.Entities;
//using System;
//using System.Collections.Generic;
//using Xunit;

//namespace ExpenseTracker.Business.Tests.TransactionTests
//{
//    public class GetTransactionListTests : BaseTransactionTest
//    {
//        [Fact]
//        public void GetTransactionsForPeriod_Success()
//        {
//            // ARRANGE
//            TransactionBusiness transactionBusiness = new TransactionBusiness(context);
//            int periodYear = 2019, periodMonth = 9;
//            DateTime periodDate = new DateTime(periodYear, periodMonth, 23);

//            // ACT
//            List<TransactionEntity> actual = transactionBusiness.GetTransactionsForPeriodByGivenDate(periodDate, DefaultUserId, DefaultTestBudgetId);

//            // ASSERT
//            Assert.NotNull(actual);
//            Assert.NotEmpty(actual);
//        }
//    }
//}
