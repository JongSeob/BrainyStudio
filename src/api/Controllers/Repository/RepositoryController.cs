using api.Helpers;
using api.Security;
using Sdk.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        ///Database configuration and connection
        private static DatabaseHelper _databaseConfig = new DatabaseHelper();
        private SqlConnection _databaseConnection = new SqlConnection(_databaseConfig.ConnString());


        // Get all Repositories of the user (Requires authorization)
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
            _databaseConnection.Open();
            List<Repository> resultsRepositories = new List<Repository>();
            using (_databaseConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(strSql, _databaseConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@userId", userId);
                    SqlDataAdapter parser = new SqlDataAdapter(sqlCommand);
                    DataTable datatable = new DataTable();
                    parser.Fill(datatable);

                    foreach (DataRow row in datatable.Rows)
                    {
                       Repository tempRepository = new Repository();
                       tempRepository.Id = Convert.ToInt32(row["Id"]);
                       tempRepository.Name = row["Name"].ToString();
                       tempRepository.Description = row["Description"].ToString();
                       tempRepository.Picture = row["Image_URL"].ToString();
                       tempRepository.OwnerId = Convert.ToInt32(row["User_ID"]);
                       resultsRepositories.Add(tempRepository);
                    }
                }
                return resultsRepositories;
            }
        }


        // Get certain repository by id (Requires authorization)
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
            _databaseConnection.Open();
            Repository resultRepository = new Repository();
            using (_databaseConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(strSql, _databaseConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@userId", userId);
                    sqlCommand.Parameters.AddWithValue("@repositoryId", id);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            resultRepository.Id = Convert.ToInt32(reader["Id"]);
                            resultRepository.Name = reader["Name"].ToString();
                            resultRepository.Description = reader["Description"].ToString();
                            resultRepository.Picture = reader["Image_URL"].ToString();
                            resultRepository.OwnerId = Convert.ToInt32(reader["User_ID"]);
                        }
                    }
                }
                return resultRepository;
            }
        }


        // Post a new repository (Requires authorization)
        // POST: api/Repository
        [Authorize]
        public void Post([FromBody]Repository value)
        {
            //Get User ID and form SQL Comand
            value.OwnerId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            String strSql = "INSERT INTO Repository (Name,Description,Image_URL,User_ID) VALUES (@name,@description,@imageUrl,@userId)";

            // Open connection
            _databaseConnection.Open();
            using (_databaseConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(strSql, _databaseConnection))
                {
                    sqlCommand.Parameters.Add("@name", value.Name);
                    sqlCommand.Parameters.Add("@description", value.Description);
                    sqlCommand.Parameters.Add("@imageUrl", value.Picture);
                    sqlCommand.Parameters.Add("@userId", value.OwnerId);
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }


        // Modify repository by id (Requires authorization)
        // PUT: api/Repository/5
        [Authorize]
        public void Put(int id, [FromBody]Repository value)
        {
            //Get User ID and form SQL Comand
            value.OwnerId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            String strSql = "UPDATE Repository SET Name = @name, Description = @description , Picture = @picture WHERE Id = @id AND User_ID = @userId;";

            //Open connection and update data
            _databaseConnection.Open();
            using (_databaseConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(strSql, _databaseConnection))
                {
                    sqlCommand.Parameters.Add("@id", id);
                    sqlCommand.Parameters.Add("@userid", value.OwnerId);
                    sqlCommand.Parameters.Add("@name", value.Name);
                    sqlCommand.Parameters.Add("@description", value.Description);
                    sqlCommand.Parameters.Add("@picture", value.Picture);
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }


        // Delete repository by id (Requires authorization)
        // DELETE: api/Repository/5
        [Authorize]
        public void Delete(int id)
        {
            //Get User ID and form SQL Comand
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            string strSql = "DELETE FROM Repository WHERE Id = @RepositoryId AND User_ID = @UserId";

            //If user is admin list all repositories no matter what user is the owner
            if (HttpContext.Current.User.IsInRole("Administrator"))
                strSql = "DELETE FROM Repository WHERE Id = @RepositoryId";

            //Open MSSQL Connection and delete data
            _databaseConnection.Open();
            using (_databaseConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(strSql, _databaseConnection))
                {
                    sqlCommand.Parameters.Add("@RepositoryId", id);
                    sqlCommand.Parameters.Add("@UserId", userId);
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }


    }
}