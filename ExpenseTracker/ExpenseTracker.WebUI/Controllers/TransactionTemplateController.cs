using ExpenseTracker.Business;
using ExpenseTracker.WebUI.Models.TransactionTemplate;
using System.Web.Mvc;

namespace ExpenseTracker.WebUI.Controllers
{
    public class TransactionTemplateController : BaseAuthenticatedController
    {
        readonly TransactionTemplateBusiness transactionTemplateBusiness;

        public TransactionTemplateController()
        {
            transactionTemplateBusiness = new TransactionTemplateBusiness(context);
        }

        [HttpPost]
        public JsonResult Create(CreateModel model)
        {
            bool success = transactionTemplateBusiness.CreateTransactionTemplate(model.TemplateName, model.Amount, model.Description, model.CategoryId, model.SourceAccountId, null, ActiveBudgetId, UserId);
            if (success)
            {
                return Json("success");
            }
            else
            {
                return Json("fail");
            }
        }
    }
}