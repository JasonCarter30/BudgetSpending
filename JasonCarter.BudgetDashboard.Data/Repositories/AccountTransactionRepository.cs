using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace JasonCarter.BudgetDashboard.Data.Repositories
{
    public class AccountTransactionRepository : IDataRepository, IDisposable
    {
        private string _connectionString = "";

        private IDbTransaction transaction = null;
        private IDbConnection connection = null;


        public AccountTransactionRepository(string connectionString)
        {
            _connectionString = connectionString;
            connection = new SqlConnection(_connectionString);
            connection.Open();
        }


        public T Update<T>(Type entityType, T entity) where T : class
        {
            return Update(entityType, "dbo", entity);
        }

        public T Update<T>(Type entityType, string schema, T entity) where T : class
        {
            T item = null;

            string sqlUpdate = string.Format("UPDATE [{0}].[{1}] SET ", schema, entityType.Name);
            string sqlUpdateFields = "";
            string sqlUpdateWhere = "";
            string primaryKeyId = entityType.Name + "Id";
            PropertyInfo[] properties = entity.GetType().GetProperties();

            var result = properties.Where(x => x.Name != primaryKeyId).Select(x => "[" + x.Name + "] = @" + x.Name);
            sqlUpdateFields += string.Join(", ", result);

            sqlUpdateWhere += string.Format(" WHERE {0} = @{0}", primaryKeyId);
            sqlUpdate += sqlUpdateFields + sqlUpdateWhere;

            int result2 = connection.Execute(sqlUpdate, entity, transaction);

            return item;
        }

        public int Insert<T>(Type entityType, string schema, T entity) where T : class
        {
            string sqlInsert = string.Format("INSERT INTO [{0}].[{1}]", schema, entityType.Name);
            string sqlInsertFields = "";
            string sqlInsertValues = "";

            string primaryKeyId = entityType.Name + "Id";


            PropertyInfo[] propertyInfo = entity.GetType().GetProperties().Where(x => x.PropertyType != typeof(byte[])).ToArray();

            Type primaryKeyType = propertyInfo.Where(x => x.Name == primaryKeyId).FirstOrDefault().PropertyType;


            var result = primaryKeyType != typeof(Guid) ? propertyInfo.Where(x => x.Name != primaryKeyId).Select(x => "[" + x.Name + "]") : propertyInfo.Select(x => "[" + x.Name + "]");
            sqlInsertFields += "(";
            sqlInsertFields += string.Join(",", result);
            sqlInsertFields += ")";

            result = primaryKeyType != typeof(Guid) ? propertyInfo.Where(x => x.Name != primaryKeyId).Select(x => "@" + x.Name) : propertyInfo.Select(x => "@" + x.Name);
            sqlInsertValues += " VALUES(";
            sqlInsertValues += string.Join(",", result);
            sqlInsertValues += ")";

            sqlInsert += sqlInsertFields + sqlInsertValues + ";";

            sqlInsert += "SELECT CAST(SCOPE_IDENTITY() AS INT);";

            int identity = connection.QuerySingle<int>(sqlInsert, entity, transaction);

            return identity;
        }

        public int Insert<T>(Type entityType, T entity) where T : class
        {
            return Insert(entityType, "dbo", entity);
        }

        public void BeginTransaction()
        {
            transaction = connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            
            transaction.Commit();
        }

        public T Delete<T>(Type entityType, T entity) where T : class
        {
            return Delete(entityType, "dbo", entity);
        }

        public T Delete<T>(Type entityType, string schema, T entity) where T : class
        {
            T item = null;

            string sqlUpdate = string.Format("DELETE FROM [{0}].[{1}] ", schema, entityType.Name);
            string sqlUpdateWhere = "";
            string primaryKeyId = entityType.Name + "Id";

            sqlUpdateWhere += string.Format(" WHERE {0} = @{0}", primaryKeyId);
            sqlUpdate += sqlUpdateWhere;

            int result2 = connection.Execute(sqlUpdate, entity);

            return item;
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
        public T GetById<T>(int Id, Type entityType) where T : class
        {
            T item;

            string tableName = entityType.Name;

            string whereClause = string.Format(" WHERE {0}{1}={2}", tableName, "Id", Id);

            item = connection.Query<T>(string.Format("SELECT * FROM {0}{1}", tableName, whereClause)).FirstOrDefault();

            return item;
        }

        public void RollbackTransaction()
        {
            transaction.Rollback();
        }

     

        public IEnumerable<T> GetDebitCreditTotalsGroupByMonth<T>(Type type) where T: class
        {
            string storedProcedure = "GetDebitCreaditTotalsByMonth";
            var items = connection.Query<T>(storedProcedure, commandType: CommandType.StoredProcedure);
            return items;
        }



        public dynamic ExecuteStoredProcedureCommand(string commandText, IDictionary<string, object>parameters)
        {
            var items = connection.Query(commandText, parameters, commandType: CommandType.StoredProcedure);
            return items;
        }


        public dynamic GetYearlyTransactionSourceSummary(string commandText)
        {
            var items = connection.Query(commandText, commandType: CommandType.StoredProcedure);
            return items;
        }

        public IEnumerable<T> GetAll<T>(Type entityType, string schema) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
