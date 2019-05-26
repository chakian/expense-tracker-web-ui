using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Context.DbModels;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Business
{
    public class CurrencyBusiness : BaseBusiness
    {
        public CurrencyBusiness(ExpenseTrackerContext context) : base(context) { }

        public List<Currency> GetCurrencyList()
        {
            return context.Currencies.Where(c => c.IsActive).ToList();
        }
    }
}
