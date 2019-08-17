using ExpenseTracker.Entities;
using ExpenseTracker.Persistence.Context;

namespace ExpenseTracker.Business
{
    public class UserBusiness : BaseBusiness
    {
        public UserBusiness() : base() { }

        public UserBusiness(ExpenseTrackerContext context) : base(context) { }

        public UserEntity GetUserById(string userId)
        {
            var user = context.Users.Find(userId);

            if (user == null)
            {
                return null;
            }

            UserEntity userEntity = mapper.Map<UserEntity>(user);
            return userEntity;
        }
    }
}
