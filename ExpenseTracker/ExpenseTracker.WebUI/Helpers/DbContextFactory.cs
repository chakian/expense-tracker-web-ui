using ExpenseTracker.Persistence.Context;

namespace ExpenseTracker.WebUI.Helpers
{
    public class DbContextFactory
    {
        public static ExpenseTrackerContext GetExpenseTrackerContext()
        {
            return new ExpenseTrackerContext();
        }
    }
}