using System.Collections.Generic;
using System.Linq;
using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Context.DbModels;

namespace ExpenseTracker.Business
{
    public class AccountTypeBusiness : BaseBusiness
    {
        #region constructor
        public AccountTypeBusiness() { }

        public AccountTypeBusiness(ExpenseTrackerContext context) : base(context) { }
        #endregion

        #region Private Methods
        private List<AccountType> GetAccountTypeList()
        {
            return context.AccountTypes.Where(c => c.IsActive).ToList();
        }
        #endregion

        #region Internal Methods
        #endregion
    }
}
