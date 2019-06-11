using System.Web.Mvc;

namespace ExpenseTracker.WebUI.Controllers
{
    [Authorize]
    public class BaseAuthenticatedController : BaseController
    {
    }
}