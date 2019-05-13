using ExpenseTracker.WebUI.Models;
using System.Web.Mvc;

namespace ExpenseTracker.WebUI.Controllers
{
    [RequireHttps]
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