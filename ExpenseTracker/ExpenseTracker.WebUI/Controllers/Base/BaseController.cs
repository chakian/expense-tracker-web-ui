using ExpenseTracker.WebUI.Models;
using System.Web.Mvc;

namespace ExpenseTracker.WebUI.Controllers
{
    public class BaseController : Controller
    {
        protected void SetAuthenticationValues(BaseModel model)
        {
            model.User = new Entities.UserEntity()
            {
                ID = 1,
                Email = "test"
            };
        }
    }
}