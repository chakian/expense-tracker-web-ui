using ExpenseTracker.Business;
using ExpenseTracker.Common.Helpers;
using ExpenseTracker.Persistence.Context.DbModels;
using ExpenseTracker.WebUI.Models.BudgetRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ExpenseTracker.WebUI.Controllers
{
    public class BudgetPlanController : BaseAuthenticatedController
    {
        private readonly BudgetPlanBusiness budgetPlanBusiness;
        private readonly TransactionBusiness transactionBusiness;

        public BudgetPlanController()
        {
            budgetPlanBusiness = new BudgetPlanBusiness(context);
            transactionBusiness = new TransactionBusiness(context);
        }

        [HttpGet]
        [Route("BudgetPlan", Name = "CurrentBudgetPlan", Order = 0)]
        [Route("BudgetPlan/{budgetPlanId:int}", Name = "BudgetPlanById", Order = 1)]
        [Route("BudgetPlan/{year:int}/{month:int}", Name = "BudgetPlanByYearAndMonth", Order = 2)]
        public ActionResult Update(int? budgetPlanId, int? year, int? month)
        {
            BudgetPlanUpdateModel model = new BudgetPlanUpdateModel()
            {
                BudgetPlan = new Models.ContextObjects.BudgetPlan(),
                PlanCategories = new List<Models.ContextObjects.BudgetPlanCategory>()
            };

            BudgetPlan budgetPlan = null;

            if (budgetPlanId.HasValue)
            {
                budgetPlan = budgetPlanBusiness.GetBudgetPlanById(budgetPlanId.Value, UserId);
            }
            else if (!year.HasValue && !month.HasValue)
            {
                year = DateTime.Now.Year;
                month = DateTime.Now.Month;
            }

            if (budgetPlan == null)
            {
                budgetPlan = budgetPlanBusiness.GetBudgetPlanByYearAndMonth(ActiveBudgetId, year.Value, month.Value, UserId);
            }

            if (budgetPlan != null)
            {
                model.BudgetPlan.BudgetPlanId = budgetPlan.BudgetPlanId;

                model.BudgetPlan.Month = budgetPlan.Month;
                model.BudgetPlan.MonthName = DateHelper.GetMonthNameByIndex(budgetPlan.Month);
                model.BudgetPlan.Year = budgetPlan.Year;

                DateTime currentDateTime = new DateTime(budgetPlan.Year, budgetPlan.Month, 1);
                DateTime previousDateTime = currentDateTime.AddMonths(-1);
                DateTime nextDateTime = currentDateTime.AddMonths(1);
                model.BudgetPlan.PreviousMonth = previousDateTime.Month;
                model.BudgetPlan.PreviousYear = previousDateTime.Year;
                model.BudgetPlan.NextMonth = nextDateTime.Month;
                model.BudgetPlan.NextYear = nextDateTime.Year;

                var currentPeriodTransactionsGroupedList = transactionBusiness.GetTransactionsForPeriodByGivenDate_GroupedByCategory(currentDateTime, UserId, ActiveBudgetId);

                foreach (var bpCategory in budgetPlan.BudgetPlanCategories)
                {
                    var transactionForCategory = currentPeriodTransactionsGroupedList.SingleOrDefault(t=>t.CategoryId.Equals(bpCategory.CategoryId));
                    var category = new Models.ContextObjects.BudgetPlanCategory()
                    {
                        BudgetPlanCategoryId = bpCategory.BudgetPlanCategoryId,
                        CategoryId = bpCategory.CategoryId,
                        CategoryName = bpCategory.Category.Name,
                        PlannedAmount = bpCategory.PlannedAmount,
                        SpentAmount = (transactionForCategory?.Amount ?? 0) * (-1)
                    };
                    category.RemainingAmount = category.PlannedAmount - category.SpentAmount;

                    model.PlanCategories.Add(category);
                }

                return View(model);
            }

            return RedirectToAction("Update");
        }

        [HttpPost]
        [Route("BudgetPlan")]
        [ValidateAntiForgeryToken]
        public ActionResult Update(BudgetPlanUpdateModel model)
        {
            List<BudgetPlanCategory> budgetPlanCategories = new List<BudgetPlanCategory>();
            model.PlanCategories.ForEach(category =>
            {
                budgetPlanCategories.Add(new BudgetPlanCategory()
                {
                    BudgetPlanCategoryId = category.BudgetPlanCategoryId,
                    PlannedAmount = category.PlannedAmount
                });
            });
            budgetPlanBusiness.UpdatePlan(budgetPlanCategories, UserId);

            return RedirectToAction("Update", new { model.BudgetPlan.BudgetPlanId });
        }
    }
}
