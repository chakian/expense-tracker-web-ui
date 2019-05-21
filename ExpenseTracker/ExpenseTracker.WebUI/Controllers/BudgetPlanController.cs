using ExpenseTracker.WebUI.Models.BudgetRelated;
using System.Web.Mvc;

namespace ExpenseTracker.WebUI.Controllers
{
    public class BudgetPlanController : BaseController
    {
        [Route("BudgetPlan", Name = "CurrentBudgetPlan", Order = 0)]
        [Route("BudgetPlan/{budgetPlanId:int}", Name = "BudgetPlanById", Order = 1)]
        [Route("BudgetPlan/{year:int}/{month:int}", Name = "BudgetPlanByYearAndMonth", Order = 2)]
        public ActionResult Update(int? budgetPlanId, int? year, int? month)
        {
            BudgetPlanUpdateModel model = new BudgetPlanUpdateModel()
            {
                BudgetPlan = new Models.ContextObjects.BudgetPlan(),
                Categories = new System.Collections.Generic.List<Models.ContextObjects.BudgetPlanCategory>()
            };



            model.BudgetPlan.Month = 5;
            model.BudgetPlan.Year = 2019;




            return View(model);
        }
    }
}
