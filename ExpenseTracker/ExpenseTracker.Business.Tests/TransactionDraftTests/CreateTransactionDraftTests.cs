using ExpenseTracker.Persistence.Context.DbModels;
using ExpenseTracker.Persistence.Identity;
using Xunit;

namespace ExpenseTracker.Business.Tests.TransactionDraftTests
{
    public class CreateTransactionDraftTests : BaseQueryTest
    {
        private readonly Currency DefaultCurrency;
        private readonly User DefaultUser;
        private readonly Budget DefaultBudget;
        private readonly AccountType DefaultAccountType;
        private readonly Account DefaultAccount;

        public CreateTransactionDraftTests()
        {
            // Create currency
            var currency = new Currency { IsActive = true, CurrencyId = 1, CurrencyCode = "TRY", DisplayName = "TL", LongName = "Türk Lirası" };
            context.Currencies.Add(currency);
            context.SaveChanges();
            DefaultCurrency = currency;

            // Create user
            var user = new User { IsActive = true, UserName = "testUser", Id = "123", Email = "test@Defa.ult" };
            context.Users.Add(user);
            context.SaveChanges();
            DefaultUser = user;

            // Create budget for user
            var budget = new BudgetBusiness(context).CreateBudget("testBudget", DefaultCurrency.CurrencyId, DefaultUser.Id);
            DefaultBudget = budget;

            // Create an account type
            var accountType = new AccountType { AccountTypeId = 1, Name = "Cash", IsActive = true };
            context.AccountTypes.Add(accountType);
            context.SaveChanges();
            DefaultAccountType = accountType;

            // Create account for user and budget
            var account = CreateNewAuthorizedEntity<Account>(DefaultUser.Id);
            account.BudgetId = DefaultBudget.BudgetId;
            account.CurrentBalance = account.StartingBalance = 5000;
            account.Name = "testAccount";
            account.AccountTypeId = DefaultAccountType.AccountTypeId;
            context.Accounts.Add(account);
            context.SaveChanges();
            DefaultAccount = account;
        }

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
        public void CreateTransactionDraft_Success(double? _amount, string description, int? categoryId, int? sourceAccountId)
        {
            //"decimal?" type was not an option, so I had to use "double?"
            decimal? amount = (decimal?)_amount;

            // Arrange
            var business = new TransactionDraftBusiness(context);

            // Act
            var actualResult = business.CreateTransactionDraft(amount, description, categoryId, sourceAccountId, DefaultBudget.BudgetId, DefaultUser.Id);

            // Assert
            Assert.NotNull(actualResult);
        }

        [Fact]
        public void CreateTransactionDraft_Fail()
        {
        }
    }
}
