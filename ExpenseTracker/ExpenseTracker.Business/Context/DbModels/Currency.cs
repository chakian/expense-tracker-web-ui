using System.Collections.Generic;

namespace ExpenseTracker.Business.Context.DbModels
{
    public class Currency : LookupEntity
    {
        public virtual List<Account> Accounts { get; set; }
    }
}
