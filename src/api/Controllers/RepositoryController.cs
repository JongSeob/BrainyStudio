using api.Helpers;
using api.Security;
using Sdk.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace api.Controllers
{
    [Authorize]
    public class RepositoryController : ApiController
    {
        /// Global helpers for Database configuration, connection and data serialization
        private static readonly DatabaseHelper DbConfig = new DatabaseHelper();
        private static readonly SqlConnection DatabaseConnection = new SqlConnection(DbConfig.ConnString());

        // Get all Repositories of the user (Requires Authorization)
        // GET: api/Repository
        [Authorize]
        public IEnumerable<Repository> Get()
        {
            //Get User ID and form SQL Comand
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            string strSql = "SELECT * FROM Repository WHERE User_ID = @userId";

            //If user is admin list all repositories no matter what user is the owner
            if (HttpContext.Current.User.IsInRole("Administrator"))
            {
                strSql = "SELECT * FROM Repository";
            }

            //Open MSSQL Connection and obtain data
            DatabaseConnection.Open();
            List<Repository> results = new List<Repository>();
            using (DatabaseConnection)
            {
                using (SqlCommand cmd = new SqlCommand(strSql, DatabaseConnection))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    SqlDataAdapter parser = new SqlDataAdapter(cmd);
                    DataTable datatable = new DataTable();
                    parser.Fill(datatable);

                    foreach (DataRow row in datatable.Rows)
                        results.Add(new Repository(
                            row.Field<int>(0),
                            row.Field<string>(1),
                            row.Field<string>(2),
                            row.Field<string>(3)
                            ));
                }
                return results;
            }
        }


        // Get certain repository by id (Requires Authorization)
        // GET: api/Repository/5
        [Authorize]
        public Repository Get(int id)
        {
            //Get User ID and form SQL Comand
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            string strSql = "SELECT * FROM Repository WHERE Id = @repositoryId AND User_ID = @userId";

            //If user is admin list all repositories no matter what user is the owner
            if (HttpContext.Current.User.IsInRole("Administrator"))
            {
                strSql = "SELECT * FROM Repository WHERE Id = @repositoryId";
            }

            //Open MSSQL Connection and obtain data
            DatabaseConnection.Open();
            Repository results = new Repository();
            using (DatabaseConnection)
            {
                using (SqlCommand cmd = new SqlCommand(strSql, DatabaseConnection))
                {
                    cmd.Parameters.AddWithValue("@userId", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results = new Repository(Int32.Parse(reader["Id"].ToString()),
                                reader["Name"].ToString(), reader["Description"].ToString(),
                                reader["Picture"].ToString());
                        }
                    }
                }
                return results;
            }
        }


        // Post a new repository (Requires Authorization)
        // POST: api/Repository
        [Authorize]
        public void Post([FromBody]Repository value)
        {
            //Get User ID and form SQL Comand
            value.OwnerId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            String strSQL = "INSERT INTO Repository (Name,Description,Image_URL,User_ID) VALUES (@Name,@Description,@ImageUrl,@UserId)";

            // Open connection
            DatabaseConnection.Open();
            using (DatabaseConnection)
            {
                using (SqlCommand sql_command = new SqlCommand(strSQL, DatabaseConnection))
                {
                    sql_command.Parameters.Add("@name", value.Name);
                    sql_command.Parameters.Add("@description", value.Description);
                    sql_command.Parameters.Add("@ImageUrl", value.Picture);
                    sql_command.Parameters.Add("@UserId", value.OwnerId);
                    sql_command.ExecuteNonQuery();
                }
            }
        }


        // Modify repository by id (Requires Authorization)
        // PUT: api/Repository/5
        [Authorize]
        public void Put(int id, [FromBody]Repository value)
        {



            DatabaseConnection.Open();
            String strSQL = "UPDATE Repository SET Name = @name, Description = @description , Picture = @picture WHERE Id = @id AND User_ID = @UserID;";

            using (DatabaseConnection)
            {
                using (SqlCommand sql_command = new SqlCommand(strSQL, DatabaseConnection))
                {
                    sql_command.Parameters.Add("@id", id);
                    //sql_command.Parameters.Add("@UserID", UserID);
                    sql_command.Parameters.Add("@name", value.Name);
                    sql_command.Parameters.Add("@description", value.Description);
                    sql_command.Parameters.Add("@picture", value.Picture);
                    sql_command.ExecuteNonQuery();
                }
            }
        }


        // Delete repository by id (Requires Authorization)
        // DELETE: api/Repository/5
        [Authorize]
        public void Delete(int id)
        {
            //Get User ID and form SQL Comand
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            string strSql = "DELETE FROM Repository WHERE Id = @RepositoryId AND User_ID = @UserId";

            //If user is admin list all repositories no matter what user is the owner
            if (HttpContext.Current.User.IsInRole("Administrator"))
            {
                strSql = "DELETE FROM Repository WHERE Id = @RepositoryId";
            }

            //Open MSSQL Connection and delete data
            DatabaseConnection.Open();
            using (DatabaseConnection)
            {
                using (SqlCommand sql_command = new SqlCommand(strSql, DatabaseConnection))
                {
                    sql_command.Parameters.Add("@RepositoryId", id);
                    sql_command.Parameters.Add("@UserId", userId);
                    sql_command.ExecuteNonQuery();
                }
            }
        }
    }
}