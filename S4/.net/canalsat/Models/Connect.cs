using System;


using Microsoft.Data.SqlClient;
using System.Text;
namespace canalsat.Models
{
    public class Connect
    {
        public SqlConnection connectDB()
        {

            var datasource = @".\sqlexpress";
            var database = "CANALSAT";

            //your connection string
            string connString = @"Data Source=" + datasource + ";Initial Catalog="
                        + database + ";Persist Security Info=True; Trusted_Connection=True; TrustServerCertificate=True";

            //create instanace of database connection
            SqlConnection conn = new SqlConnection(connString);


            try
            {
                //open connection
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            return conn;
        }
    }
}
