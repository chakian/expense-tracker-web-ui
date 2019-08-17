using ExpenseTracker.Entities;
using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Context.DbModels;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Business
{
    public class CurrencyBusiness : BaseBusiness
    {
        #region constructor
        public CurrencyBusiness() : base() { }

        public CurrencyBusiness(ExpenseTrackerContext context) : base(context) { }
        #endregion

        #region Private Methods
        #endregion

        #region Internal Methods
        #endregion

        public List<CurrencyEntity> GetCurrencyList()
        {
            return mapper.Map<List<CurrencyEntity>>(context.Currencies.Where(c => c.IsActive).ToList());
        }
    }
}
