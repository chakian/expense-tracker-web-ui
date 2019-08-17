using System.Collections.Generic;
using System.Linq;
using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Context.DbModels;

namespace ExpenseTracker.Business
{
    public class AccountTypeBusiness : BaseBusiness
    {
        public AccountTypeBusiness() { }

        public AccountTypeBusiness(ExpenseTrackerContext context) : base(context) { }

        public List<AccountType> GetAccountTypeList()
        {
            return context.AccountTypes.Where(c => c.IsActive).ToList();
        }
    }
}
