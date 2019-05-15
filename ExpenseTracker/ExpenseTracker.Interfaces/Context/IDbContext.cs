using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace ExpenseTracker.Interfaces.Context
{
    public interface IDbContext : IDisposable
    {
        IDbSet<T> Set<T>() where T : class;
        DbEntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}
