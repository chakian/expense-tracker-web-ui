using System;

namespace ExpenseTracker.Entities
{
    public class ExpenseTrackerRequest<T>
    {
        public T Data { get; private set; }

        public string RequestIP { get; set; }
        public string RequestingUserID { get; set; }
        public DateTime RequestDate { get; set; }

        public ExpenseTrackerRequest(T requestInput)
        {
            Data = requestInput;
        }
    }
}
