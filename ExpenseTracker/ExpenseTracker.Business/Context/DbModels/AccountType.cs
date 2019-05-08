using System.Collections.Generic;

namespace ExpenseTracker.Business.Context.DbModels
{
    public class AccountType : LookupEntity
    {
        public virtual List<Account> Accounts { get; set; }
    }
}
