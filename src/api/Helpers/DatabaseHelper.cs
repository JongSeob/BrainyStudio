using Sdk.Models;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Http.Headers;
using System.Text;

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
            return "Data Source=" + DbServer + ";Database=" + DbDatabase + ";User ID=" + DbUser + ";Password="
                + DbPassword + ";Integrated Security=True";
        }

        /// <summary>
        /// Validates database connection
        /// </summary>
        private void Validate()
        {
            var databaseConn = new SqlConnection();
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

        /// <summary>
        /// Returns UserID from Database based on HTTP request webauth
        /// </summary>
        public int GetUserIdFromHeader(AuthenticationHeaderValue headerValues)
        {
            string[] decodedCredentials
                    = Encoding.ASCII.GetString(Convert.FromBase64String(
                    headerValues.Parameter))
                    .Split(new[] { ':' });

            SqlConnection myConnection = new SqlConnection(ConnString());
            myConnection.Open();
            string strSql = "SELECT Id FROM [User] WHERE Nickname = @userId AND Password = @userPass";

            using (myConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(strSql, myConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@userId", decodedCredentials[0]);
                    sqlCommand.Parameters.AddWithValue("@userPass", decodedCredentials[1]);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return Int32.Parse(reader["Id"].ToString()); // Return ID of the User
                        }
                    }
                }
                return -1; // User does not exist.
            }
        }

        /// <summary>
        /// Returns UserID from Database based on HTTP request webauth
        /// </summary>
        public User GetUserFromHeader(AuthenticationHeaderValue headerValues)
        {
            string[] decodedCredentials
                    = Encoding.ASCII.GetString(Convert.FromBase64String(
                    headerValues.Parameter))
                    .Split(new[] { ':' });

            SqlConnection myConnection = new SqlConnection(ConnString());
            myConnection.Open();
            string strSql = "SELECT * FROM [User] WHERE Nickname = @userId AND Password = @userPass";

            using (myConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(strSql, myConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@userId", decodedCredentials[0]);
                    sqlCommand.Parameters.AddWithValue("@userPass", decodedCredentials[1]);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User userResult = new User(
                                Int32.Parse(reader["Id"].ToString()),
                                reader["Nickname"].ToString(),
                                reader["Role"].ToString(),
                                reader["Name"].ToString(),
                                reader["Avatar_URL"].ToString(),
                                reader["Notes"].ToString(),
                                reader["Url"].ToString());

                            return userResult;
                        }
                    }
                }
                return null; // User does not exist.
            }
        }
    }
}