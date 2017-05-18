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
        ///Database configuration and connection
        private static DatabaseHelper _databaseConfig = new DatabaseHelper();
        private SqlConnection _databaseConnection = new SqlConnection(_databaseConfig.ConnString());


        // Get profile of logged user (Requires Authorization)
        // GET: api/User
        [Authorize]
        public User Get()
        {
            //Get User ID and form SQL Comand
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            string strSql = "SELECT * FROM [User] WHERE Id = @userId";

            //Open MSSQL Connection and obtain data
            _databaseConnection.Open();
            User userResult = new User();

            using (_databaseConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(strSql, _databaseConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@userId", userId);
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userResult = new User(
                                Int32.Parse(reader["Id"].ToString()),
                                reader["Nickname"].ToString(),
                                reader["Role"].ToString(),
                                reader["Name"].ToString(),
                                reader["Avatar_URL"].ToString(),
                                reader["Notes"].ToString(),
                                reader["Url"].ToString());
                        }
                    }
                }
                return userResult;
            }
        }


        // Get profile of user by Id (Requires Authorization)
        // GET: api/User/5
        [Authorize]
        public User Get(int id)
        {
            //Get User ID and form SQL Comand
            string strSql = "SELECT * FROM [User] WHERE Id = @userId";

            //Open MSSQL Connection and obtain data
            _databaseConnection.Open();
            User resultUser = new User();
            using (_databaseConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(strSql, _databaseConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@userId", id);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            resultUser.Id = Convert.ToInt32(reader["Id"]);
                            resultUser.Nickname = reader["Nickname"].ToString();
                            resultUser.Role = reader["Role"].ToString();
                            resultUser.Name = reader["Name"].ToString();
                            resultUser.AvatarUrl = reader["Avatar_URL"].ToString();
                            resultUser.Notes = reader["Notes"].ToString();
                            resultUser.Url = reader["Url"].ToString();
                        }
                    }
                }
                return resultUser;
            }
        }


        // Create a new user account (Anonymous access permited)
        // POST: api/User
        [AllowAnonymous]
        public void Post([FromBody]User value)
        {
            //Form SQL Comand
            String strSql = "INSERT INTO [User] (Nickname,Name,Role,Avatar_URL,Notes,Url,Password) VALUES (@nickname,@name,@role,@avatarUrl,@notes,@url,@password)";

            // Open connection
            _databaseConnection.Open();
            using (_databaseConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(strSql, _databaseConnection))
                {
                    sqlCommand.Parameters.Add("@nickname", value.Nickname);
                    sqlCommand.Parameters.Add("@name", value.Name);

                    //If user is not admin he cant change the role
                    if (HttpContext.Current.User.IsInRole("Administrator"))
                        sqlCommand.Parameters.Add("@role", value.Role);
                    else sqlCommand.Parameters.Add("@role", "User");

                    sqlCommand.Parameters.Add("@avatarUrl", value.AvatarUrl);
                    sqlCommand.Parameters.Add("@notes", value.Notes);
                    sqlCommand.Parameters.Add("@url", value.Url);
                    sqlCommand.Parameters.Add("@password", value.Password);

                    sqlCommand.ExecuteNonQuery();
                }
            }

        }


        // Edit profile of logged user (Requires Authorization)
        // PUT: api/User/5
        [Authorize]
        public void Put(int id, [FromBody]User value)
        {
            //Form SQL Comand
            String strSql = "UPDATE [User] SET Nickname = @nickname, Name = @name, Role = @role , Avatar_URL = @avatarUrl , Notes = @notes, Url = @url, Password = @password WHERE Id = @id";

            //If user is not admin he can only edit himself
            if (HttpContext.Current.User.IsInRole("Administrator")) value.Id = id;
            else value.Id = Convert.ToInt32(HttpContext.Current.User.Identity.Name);

            // Open connection
            _databaseConnection.Open();
            using (_databaseConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(strSql, _databaseConnection))
                {
                    sqlCommand.Parameters.Add("@id", value.Id);
                    sqlCommand.Parameters.Add("@nickname", value.Nickname);
                    sqlCommand.Parameters.Add("@name", value.Name);

                    //If user is not admin he cant change the role
                    if (HttpContext.Current.User.IsInRole("Administrator"))
                        sqlCommand.Parameters.Add("@role", value.Role);
                    else sqlCommand.Parameters.Add("@role", "User");

                    sqlCommand.Parameters.Add("@avatarUrl", value.AvatarUrl);
                    sqlCommand.Parameters.Add("@notes", value.Notes);
                    sqlCommand.Parameters.Add("@url", value.Url);
                    sqlCommand.Parameters.Add("@password", value.Password);

                    sqlCommand.ExecuteNonQuery();
                }
            }
        }


        // Delete user by Id (Requires Authorization as an Admin)
        // DELETE: api/User/5
        [Authorize(Roles = "Administrator")]
        public void Delete(int id)
        {
            string strSql = "DELETE FROM [User] WHERE Id = @UserId";
            
            //Open MSSQL Connection and delete data
            _databaseConnection.Open();
            using (_databaseConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(strSql, _databaseConnection))
                {
                    sqlCommand.Parameters.Add("@UserId", id);
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}