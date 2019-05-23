using ExpenseTracker.Business;
using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Context.DbModels;
using ExpenseTracker.WebUI.Models.BudgetRelated;
using System;
using System.Web.Mvc;

namespace ExpenseTracker.WebUI.Controllers
{
    public class BudgetPlanController : BaseController
    {
        private readonly BudgetPlanBusiness budgetPlanBusiness;

        public BudgetPlanController(ExpenseTrackerContext context)
        {
            budgetPlanBusiness = new BudgetPlanBusiness(context);
        }

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

            BudgetPlan budgetPlan = null;

            if (budgetPlanId.HasValue)
            {
                budgetPlan = budgetPlanBusiness.GetBudgetPlanById(budgetPlanId.Value, UserId);
            }
            else if(!year.HasValue && !month.HasValue)
            {
                year = DateTime.Now.Year;
                month = DateTime.Now.Month;
            }

            if(budgetPlan == null)
            {
                budgetPlan = budgetPlanBusiness.GetBudgetPlanByYearAndMonth(ActiveBudgetId, year.Value, month.Value, UserId);
            }

            model.BudgetPlan.Month = budgetPlan.Month;
            model.BudgetPlan.MonthName = "";
            model.BudgetPlan.Year = budgetPlan.Year;

            return View(model);
        }
    }
}
