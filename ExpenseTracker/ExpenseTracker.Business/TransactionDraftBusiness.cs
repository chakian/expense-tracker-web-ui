using System;
using ExpenseTracker.Persistence.Context;

namespace ExpenseTracker.Business
{
    public class TransactionDraftBusiness : BaseBusiness
    {
        public TransactionDraftBusiness(ExpenseTrackerContext context) : base(context)
        {
        }

        public object CreateTransactionDraft(decimal? amount, string description, int? categoryId, int? sourceAccountId, int budgetId, string id) => throw new NotImplementedException();
    }
}
