using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Business.Context.DbModels
{
    public class Category : AuditableEntity
    {
        public string Name { get; set; }

        #region Foreign Keys
        public int BudgetID { get; set; }
        #endregion

        public Budget Budget { get; set; }
    }
}
