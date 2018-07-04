using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Data.Repositories
{
    public interface IDataRepository
    {
        T GetById<T>(int Id, Type entityType) where T : class;
        IEnumerable<T> GetAll<T>(Type entityType) where T : class;
        IEnumerable<T> GetAll<T>(Type entityType, string schema) where T : class;

        T Delete<T>(Type entityType, T entity) where T : class;
        T Delete<T>(Type entityType, string schema, T entity) where T : class;

        T Update<T>(Type entityType, string schema, T entity) where T : class;

        int Insert<T>(Type entityType, string schema, T entity) where T : class;
        int Insert<T>(Type entityType, T entity) where T : class;

        void RollbackTransaction();
        void CommitTransaction();
        void BeginTransaction();

    }
}
