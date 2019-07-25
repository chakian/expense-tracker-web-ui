using ExpenseTracker.Business;
using ExpenseTracker.Common.Helpers;
//TODO: Do not use context in Web project. Use the Entities instead!
using ExpenseTracker.Persistence.Context.DbModels;
using ExpenseTracker.WebUI.Models.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ExpenseTracker.WebUI.Controllers
{
    public class ReportController : BaseAuthenticatedController
    {
        private readonly BudgetPlanBusiness budgetPlanBusiness;
        private readonly TransactionBusiness transactionBusiness;

        public ReportController()
        {
            budgetPlanBusiness = new BudgetPlanBusiness(context);
            transactionBusiness = new TransactionBusiness(context);
        }

        [HttpGet]
        [Route("Report", Name = "CurrentMonthReport", Order = 0)]
        [Route("Report/{budgetPlanId:int}", Name = "ReportByBudgetPlanId", Order = 1)]
        [Route("Report/{year:int}/{month:int}", Name = "ReportByYearAndMonth", Order = 2)]
        public ActionResult Index(int? budgetPlanId, int? year, int? month)
        {
            BudgetPlanReportModel model = new BudgetPlanReportModel()
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

                List<Transaction> transactions = transactionBusiness.GetTransactionsForPeriodByGivenDate(currentDateTime, UserId, ActiveBudgetId);

                foreach (var bpCategory in budgetPlan.BudgetPlanCategories)
                {
                    var categoryTransactions = transactions.Where(t => t.IsActive && t.CategoryId == bpCategory.CategoryId).ToList();

                    model.PlanCategories.Add(new Models.ContextObjects.BudgetPlanCategory()
                    {
                        BudgetPlanCategoryId = bpCategory.BudgetPlanCategoryId,
                        CategoryId = bpCategory.CategoryId,
                        CategoryName = bpCategory.Category.Name,
                        PlannedAmount = bpCategory.PlannedAmount,
                        SpentAmount = categoryTransactions.Sum(t => t.Amount)
                    });
                }

                return View(model);
            }

            return RedirectToAction("Update");
        }
    }
}