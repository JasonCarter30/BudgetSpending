using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;




namespace JasonCarter.BudgetDashboard.Test.Data
{
    [TestClass]
    public class DataRepositoryTest
    {


        [TestInitialize]
        public void Initialize()
        {

        }




        [TestMethod("ConnectionStringTest")]
        public void ConnectionStringTest()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();


            builder.DataSource = "tcp:mysqlserver5150.database.windows.net,1433";
            builder.InitialCatalog = "Budget";
            builder.PersistSecurityInfo = false;
            builder.UserID = "azureuser";
            builder.Password = "Bigchair";
            builder.MultipleActiveResultSets = false;
            builder.Encrypt = true;
            builder.TrustServerCertificate = false;
            builder.ConnectTimeout = 30;


            var connectionString = "Server=tcp:mysqlserver5150.database.windows.net,1433;Initial Catalog=Budget;Persist Security Info=False;User ID=azureuser;Password=Bigchair19;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";



            using (var conn = new SqlConnection(builder.ConnectionString))
            //using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                Console.WriteLine("The connection was successfully made.");
            }
        }
    }
}
