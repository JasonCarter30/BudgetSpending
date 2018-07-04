using Dapper;
using System.Data.SqlClient;

namespace JasonCarter.BudgetDashboard.Data.DataAccess
{
    public class Helper
    {
        public static dynamic GetStoredProcedureColumns(string connectionString, string storedProcedure)
        {
            dynamic items = null;

            using (SqlConnection sqlconnection = new SqlConnection(connectionString))
            {
                sqlconnection.Open();
                string sql = string.Format("sp_describe_first_result_set N'{0}'", storedProcedure);
                items = sqlconnection.Query(string.Format(sql));
            }

            return items;
        }


        public static dynamic GetStoredProcedureColumnMetaData(string connectionString, string storedProcedure, string column)
        {
            dynamic items = null;

            using (SqlConnection sqlconnection = new SqlConnection(connectionString))
            {
                sqlconnection.Open();
                string sql = string.Format("EXEC sp_describe_first_result_set N'{0}'", storedProcedure);
                items = sqlconnection.Query(string.Format(sql));
            }

            

            return items;
        }
    }
}
