using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace JasonCarter.BudgetDashboard.Data.Repositories
{
    public class DataRepository : IDataRepository, IDisposable
    {
        private string _connectionString = "";

        private IDbTransaction transaction = null;
        private IDbConnection connection = null;

        public DataRepository(string connectionString)
        {
            _connectionString = connectionString;
            connection = new SqlConnection(_connectionString);
            connection.Open();

        }

        public void BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void CommitTransaction()
        {
            throw new NotImplementedException();
        }

        public T Delete<T>(Type entityType, T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public T Delete<T>(Type entityType, string schema, T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (transaction != null)
            {
                transaction.Dispose();
            }

            transaction = null;
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            connection.Dispose();
            connection = null;
        }

        public IEnumerable<T> GetAll<T>(Type entityType) where T : class
        {
            string tableName = entityType.Name;

            var items = connection.Query<T>(string.Format("SELECT * FROM {0}", tableName));

            return items;
        }

        public IEnumerable<T> GetAll<T>(Type entityType, string schema) where T : class
        {
            string tableName = entityType.Name;

            string sql = string.Format("SELECT * FROM {0}.[{1}]", schema, tableName);

            var items = connection.Query<T>(sql);

            return items;
        }


        public dynamic ExecuteStoredProcedure(string commandText) 
        {
            var items = connection.Query(commandText, CommandType.StoredProcedure);
            return items;
        }

        public T GetById<T>(int id, Type entityType) where T : class
        {
            string tableName = entityType.Name;

            string primaryKeyId = entityType.Name + "Id";

            string where = " WHERE " + primaryKeyId + " = " + id;

            var item = connection.QuerySingle<T>(string.Format("SELECT * FROM {0}" + where, tableName));

            return item;
        }



        public int Insert<T>(Type entityType, string schema, T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public int Insert<T>(Type entityType, T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public void RollbackTransaction()
        {
            throw new NotImplementedException();
        }

        public T Update<T>(Type entityType, string schema, T entity) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
