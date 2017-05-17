using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using Sdk.Models;

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
        public int GetUserIDFromHeader(AuthenticationHeaderValue headerValues)
        {

            string[] decodedCredentials
                    = Encoding.ASCII.GetString(Convert.FromBase64String(
                    headerValues.Parameter))
                    .Split(new[] { ':' });


            SqlConnection _myConnection = new SqlConnection(ConnString());
            _myConnection.Open();
            string strSql = "SELECT * FROM [User] WHERE Nickname = @userId AND Password = @userPass";

            using (_myConnection)
            {
                using (SqlCommand cmd = new SqlCommand(strSql, _myConnection))
                {
                    cmd.Parameters.AddWithValue("@userId", decodedCredentials[0]);
                    cmd.Parameters.AddWithValue("@userPass", decodedCredentials[1]);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return Int32.Parse(reader["ID"].ToString());
                        }
                    }
                }
                return -1;
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


            SqlConnection _myConnection = new SqlConnection(ConnString());
            _myConnection.Open();
            string strSQL = "SELECT * FROM [User] WHERE Nickname = @userId AND Password = @userPass";

            using (_myConnection)
            {
                using (SqlCommand cmd = new SqlCommand(strSQL, _myConnection))
                {
                    cmd.Parameters.AddWithValue("@userId", decodedCredentials[0]);
                    cmd.Parameters.AddWithValue("@userPass", decodedCredentials[1]);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User output = new User(Convert.ToInt32(reader["ID"]), reader["Nickname"].ToString(), reader["Name"].ToString()
                                , reader["Avatar_URL"].ToString(), reader["Notes"].ToString(), reader["Url"].ToString(),
                                reader["Role"].ToString());
                            return output;
                        }
                    }
                }
                return null;
            }
        }
    }
}