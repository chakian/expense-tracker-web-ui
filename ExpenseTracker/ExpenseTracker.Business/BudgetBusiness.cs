using ExpenseTracker.Entities;
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
        #region constructor
        public BudgetBusiness() { }

        public BudgetBusiness(ExpenseTrackerContext context) : base(context) { }
        #endregion

        #region Private Methods
        private List<Budget> GetBudgetsOfUser(string userId) => context.Budgets.Where(b => b.IsActive && b.BudgetUsers.Any(bu => bu.IsActive && bu.UserId.Equals(userId)))
                .Include(b => b.Currency)
                .ToList();

        private Budget GetBudgetDetailsInternal(int budgetId, string userId)
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
        #endregion

        #region Internal Methods
        #endregion

        public BudgetEntity GetUsersFirstBudgetAndSetAsDefault(string userId, bool setAsActive = true)
        {
            var firstBudget = GetBudgetsOfUser(userId).FirstOrDefault();
            BudgetEntity budgetEntity = null;

            if (firstBudget != null)
            {
                if (setAsActive)
                {
                    var user = new UserBusiness(context).GetUserById(userId);
                    user.ActiveBudgetId = firstBudget.BudgetId;
                    context.Entry(user).State = EntityState.Modified;
                    context.SaveChanges();
                }

                budgetEntity.BudgetId = firstBudget.BudgetId;
                budgetEntity.Name = firstBudget.Name;
            }

            return budgetEntity;
        }

        public BudgetEntity GetBudgetDetails(int budgetId, string userId)
        {
            Budget budget = context.Budgets.Find(budgetId);

            if (budget == null || !budget.BudgetUsers.Any(bu => bu.UserId.Equals(userId)))
            {
                return null;
            }

            //budget.InsertUser = context.Users.Find(budget.InsertUserId);
            //budget.UpdateUser = context.Users.Find(budget.UpdateUserId);

            return new BudgetEntity
            {
                BudgetId = budget.BudgetId,
                Name = budget.Name
            };
        }

        public BudgetEntity CreateBudget(string name, int currencyId, string userId)
        {
            Budget budget = CreateBudget_NoCommit(name, currencyId, userId);
            new BudgetUserBusiness(context).CreateBudgetUser_NoCommit(budget.BudgetId, userId, userId);
            context.SaveChanges();

            return mapper.Map<BudgetEntity>(budget);
        }

        public bool UpdateBudget(int budgetId, string name, int currencyId, string updateUserId)
        {
            Budget budget = GetBudgetDetailsInternal(budgetId, updateUserId);

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
            Budget budget = GetBudgetDetailsInternal(budgetId, updateUserId);

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

        public void SetActiveBudget(string userId, int budgetId)
        {
            var userToUpdate = context.Users.Find(userId);
            userToUpdate.ActiveBudgetId = budgetId;
            context.SaveChanges();
        }
    }
}
