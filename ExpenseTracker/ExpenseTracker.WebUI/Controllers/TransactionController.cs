﻿using ExpenseTracker.Business;
using ExpenseTracker.Entities;
using ExpenseTracker.Language;
using ExpenseTracker.WebUI.Models;
using ExpenseTracker.WebUI.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ExpenseTracker.WebUI.Controllers
{
    public class TransactionController : BaseAuthenticatedController
    {
        readonly AccountBusiness budgetAccountBusiness;
        readonly CategoryBusiness categoryBusiness;
        readonly TransactionBusiness transactionBusiness;
        readonly TransactionTemplateBusiness transactionTemplateBusiness;

        public TransactionController()
        {
            budgetAccountBusiness = new AccountBusiness();
            categoryBusiness = new CategoryBusiness();
            transactionBusiness = new TransactionBusiness();
            transactionTemplateBusiness = new TransactionTemplateBusiness();
        }

        [HttpGet]
        public ActionResult Add()
        {
            AddModel model = new AddModel();

            SetAccountListForModel(model);
            SetCategoryListForModel(model);
            SetTransactionSummaryListForModel(model, DateTime.Now);
            SetTemplateListForModel(model);

            List<TransactionEntity> currentPeriodTransactionsGroupedList = transactionBusiness.GetTransactionsForPeriodByGivenDate_GroupedByCategory(DateTime.Now, UserId, ActiveBudgetId);
            model.CategoryStatusList = new List<AddModel.CategoryStatus>();
            currentPeriodTransactionsGroupedList.ForEach(t =>
            {
                //model.CategoryStatusList.Add(new AddModel.CategoryStatus()
                //{
                //    CategoryId = t.CategoryId,
                //    SpentAmount = t.Amount * -1
                //});
                throw new Exception();
            });

            model.Date = DateTime.Now;

            return View(model);
        }

        private void SetTemplateListForModel(AddModel model)
        {
            List<TransactionTemplateEntity> templates = transactionTemplateBusiness.GetTransactionTemplates(ActiveBudgetId, UserId);
            if (templates != null)
            {
                model.TemplateListForView = new SelectList(templates.OrderBy(q => q.Name), "Id", "Name");

                model.TemplateList = new List<AddModel.TemplateProperties>();
                templates.ForEach(t =>
                {
                    model.TemplateList.Add(new AddModel.TemplateProperties()
                    {
                        TemplateId = t.Id,
                        TemplateName = t.Name,
                        Amount = t.Amount ?? 0,
                        Description = t.Description,
                        CategoryId = t.CategoryId ?? 0,
                        AccountId = t.SourceAccountId ?? 0
                    });
                });
            }
        }

        private void SetCategoryListForModel(BaseEditableTransactionModel model)
        {
            List<CategoryEntity> categories = categoryBusiness.GetCategoriesByBudgetId(ActiveBudgetId, UserId);
            model.CategoryList = new List<SelectListItem>();

            if (categories != null)
            {
                if (categories.Any(c => c.IsIncomeCategory))
                {
                    var optionGroup = new SelectListGroup() { Name = Resources.GenericText_Income };
                    foreach (CategoryEntity category in categories.Where(c => c.IsIncomeCategory))
                    {
                        model.CategoryList.Add(new SelectListItem()
                        {
                            Value = category.CategoryId.ToString(),
                            Text = category.Name,
                            Group = optionGroup
                        });
                    }
                }
                if (categories.Any(c => c.IsIncomeCategory == false))
                {
                    var optionGroup = new SelectListGroup() { Name = Resources.GenericText_Expense };
                    foreach (CategoryEntity category in categories.Where(c => c.IsIncomeCategory == false))
                    {
                        model.CategoryList.Add(new SelectListItem()
                        {
                            Value = category.CategoryId.ToString(),
                            Text = category.Name,
                            Group = optionGroup
                        });
                    }
                }
            }
        }

        private void SetAccountListForModel(BaseEditableTransactionModel model)
        {
            List<AccountEntity> accounts = budgetAccountBusiness.GetAccountsOfUser(UserId, ActiveBudgetId);
            if (accounts != null)
            {
                model.AccountList = new SelectList(accounts, "AccountId", "Name");
            }
        }

        private void SetTransactionSummaryListForModel(BaseTransactionModel model, DateTime periodDate)
        {
            model.TransactionSummaries = new List<BaseTransactionModel.TransactionSummary>();

            List<TransactionEntity> transactions = transactionBusiness.GetTransactionsForPeriodByGivenDate(periodDate, UserId, ActiveBudgetId).OrderByDescending(q => q.Date).ToList();
            if (transactions != null)
            {
                transactions.ForEach(t =>
                {
                    model.TransactionSummaries.Add(new BaseTransactionModel.TransactionSummary
                    {
                        TransactionId = t.TransactionId,
                        Date = t.Date,
                        Amount = t.Amount,
                        Account = t.SourceAccount.Name,
                        Category = t.Category.Name,
                        Description = t.Description
                    });
                });
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Add(AddModel model)
        {
            if (ModelState.IsValid)
            {
                decimal amount = (model.IsIncome == true ? 1 : -1) * model.Amount;
                transactionBusiness.InsertTransaction(UserId, model.CategoryId, model.AccountId, amount, model.Description, model.Date);
                return RedirectToAction("Add");
            }

            SetAccountListForModel(model);
            SetCategoryListForModel(model);
            SetTransactionSummaryListForModel(model, DateTime.Now);

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int? TransactionId)
        {
            EditModel model = new EditModel();

            if (!TransactionId.HasValue)
            {
                return RedirectToAction("List");
            }
            else
            {
                model.TransactionId = TransactionId.Value;

                TransactionEntity transaction = transactionBusiness.GetTransactionById(TransactionId.Value, UserId);
                if (transaction.Amount < 0)
                {
                    model.Amount = transaction.Amount * -1;
                    model.IsIncome = false;
                }
                else
                {
                    model.Amount = transaction.Amount;
                    model.IsIncome = true;
                }
                model.Date = transaction.Date;
                model.Description = transaction.Description;
                //model.CategoryId = transaction.CategoryId;
                throw new Exception();
                model.AccountId = transaction.SourceAccountId;

                SetAccountListForModel(model);
                SetCategoryListForModel(model);
                SetTransactionSummaryListForModel(model, DateTime.Now);

                return View(model);
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(EditModel model)
        {
            if (ModelState.IsValid)
            {
                decimal amount = (model.IsIncome == true ? 1 : -1) * model.Amount;
                transactionBusiness.UpdateTransaction(model.TransactionId, UserId, model.CategoryId, model.AccountId, amount, model.Description, model.Date);
                return RedirectToAction("List");
            }

            TransactionEntity transaction = transactionBusiness.GetTransactionById(model.TransactionId, UserId);
            if (transaction.Amount < 0)
            {
                model.Amount *= -1;
                model.IsIncome = false;
            }
            else
            {
                model.IsIncome = true;
            }

            SetAccountListForModel(model);
            SetCategoryListForModel(model);
            SetTransactionSummaryListForModel(model, DateTime.Now);

            return View(model);
        }

        [HttpGet]
        [Route("TransactionList", Name = "CurrentMonthList", Order = 0)]
        [Route("TransactionList/{year:int}/{month:int}", Name = "ListByYearAndMonth", Order = 1)]
        public ActionResult List(int? year, int? month)
        {
            if (!year.HasValue || !month.HasValue)
            {
                year = DateTime.Now.Year;
                month = DateTime.Now.Month;
            }

            DateTime currentDateTime = new DateTime(year.Value, month.Value, 1);
            DateTime previousDateTime = currentDateTime.AddMonths(-1);
            DateTime nextDateTime = currentDateTime.AddMonths(1);

            ListModel model = new ListModel
            {
                PreviousMonth = previousDateTime.Month,
                PreviousYear = previousDateTime.Year,
                NextMonth = nextDateTime.Month,
                NextYear = nextDateTime.Year
            };

            SetTransactionSummaryListForModel(model, currentDateTime);

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Delete(EditModel model)
        {
            if (ModelState.IsValid)
            {
                transactionBusiness.DeleteTransaction(model.TransactionId, UserId);
                return RedirectToAction("List");
            }

            //TODO: Return with an error message
            return RedirectToAction("List");
        }
    }
}
