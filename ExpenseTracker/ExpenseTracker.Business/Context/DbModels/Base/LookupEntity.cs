using System.Collections.Generic;

namespace ExpenseTracker.Business.Context.DbModels
{
    public class LookupEntity : BaseEntity
    {
        public int LookupID { get; set; }
        public string LookupValue { get; set; }
    }
}
