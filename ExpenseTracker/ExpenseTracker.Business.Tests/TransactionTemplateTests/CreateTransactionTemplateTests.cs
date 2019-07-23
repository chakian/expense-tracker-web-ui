using ExpenseTracker.Persistence.Context.DbModels;
using Xunit;

namespace ExpenseTracker.Business.Tests.TransactionTemplateTests
{
    public class CreateTransactionTemplateTests : BaseQueryTest
    {
        private readonly Budget DefaultBudget;
        private readonly AccountType DefaultAccountType;
        private readonly Account DefaultAccount;
        private readonly Category DefaultCategory;

        public CreateTransactionTemplateTests()
        {
            // Create budget for user
            var budget = new BudgetBusiness(context).CreateBudget("testBudget", DefaultCurrency.CurrencyId, DefaultUserId);
            DefaultBudget = budget;

            // Create an account type
            var accountType = new AccountType { AccountTypeId = 1, Name = "Cash", IsActive = true };
            context.AccountTypes.Add(accountType);
            context.SaveChanges();
            DefaultAccountType = accountType;

            // Create account for user and budget
            var account = CreateNewAuthorizedEntity<Account>(DefaultUserId);
            account.BudgetId = DefaultBudget.BudgetId;
            account.CurrentBalance = account.StartingBalance = 5000;
            account.Name = "testAccount";
            account.AccountTypeId = DefaultAccountType.AccountTypeId;
            context.Accounts.Add(account);
            context.SaveChanges();
            DefaultAccount = account;

            // Create a category
            var category = CreateNewAuthorizedEntity<Category>(DefaultUserId);
            category.BudgetId = DefaultBudget.BudgetId;
            category.Name = "testCategory";
            context.Categories.Add(category);
            context.SaveChanges();
            DefaultCategory = category;
        }

        [Theory]
        [InlineData(false, false, false, true)]
        [InlineData(false, false, true, false)]
        [InlineData(false, false, true, true)]
        [InlineData(false, true, false, false)]
        [InlineData(false, true, false, true)]
        [InlineData(false, true, true, false)]
        [InlineData(false, true, true, true)]
        [InlineData(true, false, false, false)]
        [InlineData(true, false, false, true)]
        [InlineData(true, false, true, false)]
        [InlineData(true, false, true, true)]
        [InlineData(true, true, false, false)]
        [InlineData(true, true, false, true)]
        [InlineData(true, true, true, false)]
        [InlineData(true, true, true, true)]
        public void CreateTransactionTemplate_Success(bool useAmount, bool useDescription, bool useCategoryId, bool useSourceAccountId)
        {
            decimal? amount = useAmount ? (decimal?)150 : null;
            string description = useDescription ? "clear description" : null;
            int? categoryId = useCategoryId ? (int?)DefaultCategory.CategoryId : null;
            int? sourceAccountId = useSourceAccountId ? (int?)DefaultAccount.AccountId : null;

            string templateName = "New Template";
            int? targetAccountId = null;

            // Arrange
            var business = new TransactionTemplateBusiness(context);

            // Act
            bool actualResult = business.CreateTransactionTemplate(templateName, amount, description, categoryId, sourceAccountId, targetAccountId, DefaultBudget.BudgetId, DefaultUserId);

            // Assert
            Assert.True(actualResult);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public void CreateTransactionTemplate_Fail(bool useInvalidCategoryId, bool useInvalidSourceAccountId)
        {
            decimal? amount = null;
            string description = null;
            int? categoryId = useInvalidCategoryId ? (int?)12346 : null;
            int? sourceAccountId = useInvalidSourceAccountId ? (int?)12347 : null;

            string templateName = "New Template";
            int? targetAccountId = null;

            // Arrange
            var business = new TransactionTemplateBusiness(context);

            // Act
            bool actualResult = business.CreateTransactionTemplate(templateName, amount, description, categoryId, sourceAccountId, targetAccountId, DefaultBudget.BudgetId, DefaultUserId);

            // Assert
            Assert.False(actualResult);
        }

        [Fact]
        public void CreateTransactionTemplate_Fail_EmptyTemplateName()
        {
            // Arrange
            decimal? amount = 1000;
            string description = "nice template";
            int? categoryId = null;
            int? sourceAccountId = null;
            string templateName = "";
            int? targetAccountId = null;

            var business = new TransactionTemplateBusiness(context);

            // Act
            bool actualResult = business.CreateTransactionTemplate(templateName, amount, description, categoryId, sourceAccountId, targetAccountId, DefaultBudget.BudgetId, DefaultUserId);

            // Assert
            Assert.False(actualResult);
        }

        [Fact]
        public void CreateTransactionTemplate_Fail_TemplateNameExistsForThisUserAndThisBudget()
        {
            // Arrange
            decimal? amount = 1000;
            string description = "nice template";
            int? categoryId = null;
            int? sourceAccountId = null;
            string templateName = "New Template 1";
            int? targetAccountId = null;

            var business = new TransactionTemplateBusiness(context);
            business.CreateTransactionTemplate(templateName, 150, "Test Expense 1", categoryId, sourceAccountId, targetAccountId, DefaultBudget.BudgetId, DefaultUserId);
            business.CreateTransactionTemplate("New Template 2", 350, "Test Expense 2", categoryId, sourceAccountId, targetAccountId, DefaultBudget.BudgetId, DefaultUserId);

            // Act
            bool actualResult = business.CreateTransactionTemplate(templateName, amount, description, categoryId, sourceAccountId, targetAccountId, DefaultBudget.BudgetId, DefaultUserId);

            // Assert
            Assert.False(actualResult);
        }
    }
}
