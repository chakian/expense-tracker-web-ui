using Xunit;

namespace ExpenseTracker.Business.Tests.TransactionDraftTests
{
    public class CreateTransactionDraftTests
    {
        [Theory]
        [InlineData(null, null, null, 1)]
        [InlineData(null, null, 2, null)]
        [InlineData(null, null, 2, 1)]
        [InlineData(null, "sigara", null, null)]
        [InlineData(null, "sigara", null, 1)]
        [InlineData(null, "sigara", 2, null)]
        [InlineData(null, "sigara", 2, 1)]
        [InlineData(15, null, null, null)]
        [InlineData(15, null, null, 1)]
        [InlineData(15, null, 2, null)]
        [InlineData(15, null, 2, 1)]
        [InlineData(15, "sigara", null, null)]
        [InlineData(15, "sigara", null, 1)]
        [InlineData(15, "sigara", 2, null)]
        [InlineData(15, "sigara", 2, 1)]
        public void CreateTransactionDraft_Success(decimal? amount, string description, int? categoryId, int? sourceAccountId)
        {
        }

        [Fact]
        public void CreateTransactionDraft_Fail()
        {
        }
    }
}
