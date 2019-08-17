using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Context.DbModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ExpenseTracker.Business
{
    public class BudgetBusiness : BaseBusiness
    {
        public BudgetBusiness() { }

        public BudgetBusiness(ExpenseTrackerContext context) : base(context) { }

        public List<Budget> GetBudgetsOfUser(string userId) => context.Budgets.Where(b => b.IsActive && b.BudgetUsers.Any(bu => bu.IsActive && bu.UserId.Equals(userId)))
                .Include(b => b.Currency)
                .ToList();

        public Budget GetBudgetDetails(int budgetId, string userId)
        {
            Budget budget = context.Budgets.Find(budgetId);

            if (budget == null || !budget.BudgetUsers.Any(bu => bu.UserId.Equals(userId)))
            {
                return null;
            }

            //budget.InsertUser = context.Users.Find(budget.InsertUserId);
            //budget.UpdateUser = context.Users.Find(budget.UpdateUserId);

            return budget;
        }

        public Budget CreateBudget(string name, int currencyId, string userId)
        {
            Budget budget = CreateBudget_NoCommit(name, currencyId, userId);
            new BudgetUserBusiness(context).CreateBudgetUser_NoCommit(budget.BudgetId, userId, userId);
            context.SaveChanges();

            return budget;
        }

        private Budget CreateBudget_NoCommit(string name, int currencyId, string userId)
        {
            Budget budget = new Budget
            {
                InsertUserId = userId,
                InsertTime = DateTime.Now,
                UpdateUserId = userId,
                UpdateTime = DateTime.Now,
                IsActive = true,

                Name = name,
                CurrencyId = currencyId
            };

            context.Budgets.Add(budget);

            return budget;
        }

        public bool UpdateBudget(int budgetId, string name, int currencyId, string updateUserId)
        {
            Budget budget = GetBudgetDetails(budgetId, updateUserId);

            if(budget != null)
            {
                budget.UpdateUserId = updateUserId;
                budget.UpdateTime = DateTime.Now;

                budget.Name = name;
                budget.CurrencyId = currencyId;

                context.Entry(budget).State = EntityState.Modified;
                context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool DeleteBudget(int budgetId, string updateUserId)
        {
            Budget budget = GetBudgetDetails(budgetId, updateUserId);

            if (budget != null)
            {
                budget.UpdateUserId = updateUserId;
                budget.UpdateTime = DateTime.Now;
                budget.IsActive = false;

                context.Entry(budget).State = EntityState.Modified;
                context.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
