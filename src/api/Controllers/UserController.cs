using System;
using api.Helpers;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Http;
using Sdk.Models;

namespace api.Controllers
{
    public class UserController : ApiController
    {
        /// Global helpers for Database configuration, connection and data serialization
        private static readonly DatabaseHelper DbConfig = new DatabaseHelper();
        private static readonly SqlConnection DatabaseConnection = new SqlConnection(DbConfig.ConnString());


        // Get profile of logged user (Requires Authorization)
        // GET: api/User
        [Authorize]
        public User Get()
        {
            //Get User ID and form SQL Comand
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            string strSql = "SELECT * FROM User WHERE Id = @userId";

            //Open MSSQL Connection and obtain data
            DatabaseConnection.Open();
            User results = new User();

            using (DatabaseConnection)
            {
                using (SqlCommand cmd = new SqlCommand(strSql, DatabaseConnection))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results = new User(Int32.Parse(reader["Id"].ToString()), reader["Nickname"].ToString(), 
                                reader["Name"].ToString(), reader["Avatar_URL"].ToString(), reader["Notes"].ToString(),
                                reader["Url"].ToString(), reader["Role"].ToString());
                        }
                    }
                }
                return results;
            }
        }


        // Get profile of user by Id (Requires Authorization)
        // GET: api/User/5
        [Authorize]
        public string Get(int id)
        {
            return "value";
        }


        // Create a new user account (Anonymous access permited)
        // POST: api/User
        [AllowAnonymous]
        public void Post([FromBody]string value)
        {
        }


        // Edit profile of logged user (Requires Authorization)
        // PUT: api/User/5
        [Authorize]
        public void Put(int id, [FromBody]string value)
        {
        }


        // Delete user by Id (Requires Authorization as an Admin)
        // DELETE: api/User/5
        [Authorize(Roles = "Administrator")]
        public void Delete(int id)
        {
            string strSql = "DELETE FROM User WHERE Id = @UserId";
            
            //Open MSSQL Connection and delete data
            DatabaseConnection.Open();
            using (DatabaseConnection)
            {
                using (SqlCommand sql_command = new SqlCommand(strSql, DatabaseConnection))
                {
                    sql_command.Parameters.Add("@UserId", id);
                    sql_command.ExecuteNonQuery();
                }
            }
        }
    }
}