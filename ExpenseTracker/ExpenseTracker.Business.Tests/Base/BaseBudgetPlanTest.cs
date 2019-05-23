using Dbo = ExpenseTracker.Persistence.Context.DbModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpenseTracker.Business.Tests.Base
{
    public class BaseBudgetPlanTest : BaseBudgetTest
    {
        [TestInitialize()]
        public new void BaseTestInitialize()
        {
            base.BaseTestInitialize();
        }
    }
}
