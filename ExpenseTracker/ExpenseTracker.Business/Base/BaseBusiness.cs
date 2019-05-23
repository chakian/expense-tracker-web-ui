using ExpenseTracker.Persistence.Context;

namespace ExpenseTracker.Business
{
    public class BaseBusiness
    {
        protected readonly ExpenseTrackerContext context;

        public BaseBusiness(ExpenseTrackerContext context)
        {
            this.context = context;
        }
    }
}
