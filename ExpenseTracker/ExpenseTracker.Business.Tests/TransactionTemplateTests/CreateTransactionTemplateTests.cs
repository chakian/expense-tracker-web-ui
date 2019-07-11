using ExpenseTracker.Persistence.Context.DbModels;
using ExpenseTracker.Persistence.Identity;
using Xunit;

namespace ExpenseTracker.Business.Tests.TransactionTemplateTests
{
    public class CreateTransactionTemplateTests : BaseQueryTest
    {
        private readonly Currency DefaultCurrency;
        private readonly User DefaultUser;
        private readonly Budget DefaultBudget;
        private readonly AccountType DefaultAccountType;
        private readonly Account DefaultAccount;
        private readonly Category DefaultCategory;

        public CreateTransactionTemplateTests()
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

            // Create a category
            var category = CreateNewAuthorizedEntity<Category>(DefaultUser.Id);
            category.BudgetId = DefaultBudget.BudgetId;
            category.Name = "testCategory";
            context.Categories.Add(category);
            context.SaveChanges();
            DefaultCategory = category;
        }

        [Theory]
        [InlineData(null, null, null, 1)]
        [InlineData(null, null, 1, null)]
        [InlineData(null, null, 1, 1)]
        [InlineData(null, "sigara", null, null)]
        [InlineData(null, "sigara", null, 1)]
        [InlineData(null, "sigara", 1, null)]
        [InlineData(null, "sigara", 1, 1)]
        [InlineData(15, null, null, null)]
        [InlineData(15, null, null, 1)]
        [InlineData(15, null, 1, null)]
        [InlineData(15, null, 1, 1)]
        [InlineData(15, "sigara", null, null)]
        [InlineData(15, "sigara", null, 1)]
        [InlineData(15, "sigara", 1, null)]
        [InlineData(15, "sigara", 1, 1)]
        public void CreateTransactionTemplate_Success(double? _amount, string description, int? categoryId, int? sourceAccountId)
        {
            //"decimal?" type was not an option, so I had to use "double?"
            decimal? amount = (decimal?)_amount;

            // Arrange
            var business = new TransactionTemplateBusiness(context);
            string templateName = "New Template";
            int? targetAccountId = null;

            // Act
            bool actualResult = business.CreateTransactionTemplate(templateName, amount, description, categoryId, sourceAccountId, targetAccountId, DefaultBudget.BudgetId, DefaultUser.Id);

            // Assert
            Assert.True(actualResult);
        }

        [Fact]
        public void CreateTransactionTemplate_Fail()
        {
        }
    }
}
