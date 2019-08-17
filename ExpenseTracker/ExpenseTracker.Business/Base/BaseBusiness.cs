using AutoMapper;
using ExpenseTracker.Business.AutoMapper;
using ExpenseTracker.Entities.Base;
using ExpenseTracker.Persistence.Context;
using ExpenseTracker.Persistence.Context.DbModels;
using System;

namespace ExpenseTracker.Business
{
    public class BaseBusiness
    {
        protected readonly ExpenseTrackerContext context;
        protected IMapper mapper;

        public BaseBusiness()
            : this(new ExpenseTrackerContext())
        {
        }

        public BaseBusiness(ExpenseTrackerContext context)
        {
            this.context = context;
            mapper = AutoMapperBootstrap.Configure();
        }

        protected T CreateAuditableContextObject<T>(string userId)
            where T : AuditableDbo, new()
        {
            T obj = new T();
            obj.InsertUserId = userId;
            obj.InsertTime = DateTime.Now;
            obj.UpdateUserId = userId;
            obj.UpdateTime = DateTime.Now;
            obj.IsActive = true;

            return obj;
        }

        protected T CreateAuditableEntityObject<T>(AuditableDbo dbo)
            where T : AuditableEntity, new()
        {
            T obj = new T();
            obj.InsertUserId = dbo.InsertUserId;
            obj.InsertTime = dbo.InsertTime;
            obj.UpdateUserId = dbo.UpdateUserId;
            obj.UpdateTime = dbo.UpdateTime;
            obj.IsActive = dbo.IsActive;

            return obj;
        }
    }
}
