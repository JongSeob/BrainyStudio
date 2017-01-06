using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace api.Helpers
{
    public class DatabaseHelper
    {
        public string DbServer;
        public string DbDatabase;
        public string DbUser;
        public string DbPassword;

        /// <summary>
        /// Generate DatabaseHelper from configuration file
        /// </summary>
        public DatabaseHelper()
        {
            DbServer = ConfigurationManager.AppSettings["DBserver"];
            DbDatabase = ConfigurationManager.AppSettings["DBdatabase"];
            DbUser = ConfigurationManager.AppSettings["DBuser"];
            DbPassword = ConfigurationManager.AppSettings["DBpassword"];

            Validate();
        }

        /// <summary>
        /// Generates SQL connection string from settings
        /// </summary>
        /// <returns></returns>
        public string ConnString()
        {
            return "Data Source=" + DbServer + ";Database=" + DbDatabase + ";User ID=" + DbUser + ";Password=" + DbPassword + ";Integrated Security=True";
        }

        /// <summary>
        /// Validates database connection
        /// </summary>
        private void Validate()
        {
            SqlConnection databaseConn;
            databaseConn = new SqlConnection();
            databaseConn.ConnectionString = ConnString();

            try
            {
                databaseConn.Open();
                Console.WriteLine("OK: Connection with database server established.");
                databaseConn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: Can not open database connection.");
            }
            Console.WriteLine();
        }
    }
}