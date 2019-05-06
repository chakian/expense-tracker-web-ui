using ExpenseTracker.WebUI.Entities;

namespace ExpenseTracker.WebUI.Models
{
    public class BaseModel
    {
        //TODO: Delete This
        public BaseModel()
        {
            User = new UserEntity()
            {
                ID = 1
            };
        }
        public bool IsLoggedIn => (User != null && User.ID > 0);

        public UserEntity User { get; set; }
    }
}
