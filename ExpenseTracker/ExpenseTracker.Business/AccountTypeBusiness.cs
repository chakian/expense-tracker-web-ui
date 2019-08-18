using System.Collections.Generic;
using System.Linq;
using ExpenseTracker.Entities;
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
        #endregion

        #region Internal Methods
        #endregion

        public List<AccountTypeEntity> GetAccountTypeList()
        {
            List<AccountType> list = context.AccountTypes.Where(c => c.IsActive).ToList();
            List<AccountTypeEntity> accountTypeEntities = mapper.Map<List<AccountTypeEntity>>(list);
            return accountTypeEntities;
        }
    }
}
