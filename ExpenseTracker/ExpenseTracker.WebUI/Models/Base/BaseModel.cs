using ExpenseTracker.WebUI.Entities;

namespace ExpenseTracker.WebUI.Models
{
    public class BaseModel
    {
        public bool IsLoggedIn => (User != null && User.ID > 0);

        public UserEntity User { get; set; }
    }
}
