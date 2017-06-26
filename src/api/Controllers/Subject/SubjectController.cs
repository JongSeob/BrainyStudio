using api.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Http;

namespace api.Controllers.Subject
{
    public class SubjectController : ApiController
    {
        ///Database configuration and connection
        private static DatabaseHelper _databaseConfig = new DatabaseHelper();

        private SqlConnection _databaseConnection = new SqlConnection(_databaseConfig.ConnString());

        // GET: api/Subject
        // Get all subjects owned by signed in user
        [Authorize]
        public IEnumerable<Sdk.Models.Subject> Get()
        {
            //Get User ID and form SQL Comand
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            string strSql = "SELECT * FROM Subject WHERE User_ID = @userId";

            //If user is admin list all repositories no matter what user is the owner
            if (HttpContext.Current.User.IsInRole("Administrator"))
            {
                strSql = "SELECT * FROM Subject";
            }

            //Open MSSQL Connection and obtain data
            _databaseConnection.Open();
            List<Sdk.Models.Subject> resultsRepositories = new List<Sdk.Models.Subject>();
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
                        Sdk.Models.Subject tempSubject = new Sdk.Models.Subject();
                        tempSubject.Id = Convert.ToInt32(row["ID"]);
                        tempSubject.Name = row["Name"].ToString();
                        tempSubject.Gender = row["Gender"].ToString();
                        tempSubject.Age = Convert.ToInt32(row["Age"]);
                        tempSubject.Description = row["Description"].ToString();
                        tempSubject.OwnerId = Convert.ToInt32(row["User_ID"]);
                        resultsRepositories.Add(tempSubject);
                    }
                }
                return resultsRepositories;
            }
        }

        // GET: api/Subject/5
        // Get a particular subject by id
        [Authorize]
        public Sdk.Models.Subject Get(int id)
        {
            //Get User ID and form SQL Comand
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            string strSql = "SELECT * FROM Subject WHERE Id = @subjectId AND User_ID = @userId";

            //If user is admin list all subjects no matter what user is the owner
            if (HttpContext.Current.User.IsInRole("Administrator"))
            {
                strSql = "SELECT * FROM Subject WHERE Id = @subjectId";
            }

            //Open MSSQL Connection and obtain data
            _databaseConnection.Open();
            Sdk.Models.Subject resultSubject = new Sdk.Models.Subject();
            using (_databaseConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(strSql, _databaseConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@userId", userId);
                    sqlCommand.Parameters.AddWithValue("@subjectId", id);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            resultSubject.Id = Convert.ToInt32(reader["ID"]);
                            resultSubject.Name = reader["Name"].ToString();
                            resultSubject.Gender = reader["Gender"].ToString();
                            resultSubject.Age = Convert.ToInt32(reader["Age"]);
                            resultSubject.Description = reader["Description"].ToString();
                            resultSubject.OwnerId = Convert.ToInt32(reader["User_ID"]);
                        }
                    }
                }
                return resultSubject;
            }
        }

        // POST: api/Subject
        // Create a new subject
        [Authorize]
        public void Post([FromBody]Sdk.Models.Subject value)
        {
            //Get User ID and form SQL Comand
            value.OwnerId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            String strSql = "INSERT INTO Subject (Name,Gender,Age,Description,User_ID) VALUES (@name,@gender,@age,@description,@userId)";

            // Open connection
            _databaseConnection.Open();
            using (_databaseConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(strSql, _databaseConnection))
                {
                    sqlCommand.Parameters.Add("@name", value.Name);
                    sqlCommand.Parameters.Add("@gender", value.Gender);
                    sqlCommand.Parameters.Add("@age", value.Age);
                    sqlCommand.Parameters.Add("@description", value.Description);
                    sqlCommand.Parameters.Add("@userId", value.OwnerId);
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        // PUT: api/Subject/5
        // Edit a subject information
        [Authorize]
        public void Put(int id, [FromBody]Sdk.Models.Subject value)
        {
            //Get User ID and form SQL Comand
            value.OwnerId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            String strSql = "UPDATE Subject SET Name = @name, Gender = @gender , Age = @age, Description = @description WHERE Id = @id AND User_ID = @userId;";

            //Open connection and update data
            _databaseConnection.Open();
            using (_databaseConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(strSql, _databaseConnection))
                {
                    sqlCommand.Parameters.Add("@name", value.Name);
                    sqlCommand.Parameters.Add("@gender", value.Gender);
                    sqlCommand.Parameters.Add("@age", value.Age);
                    sqlCommand.Parameters.Add("@description", value.Description);
                    sqlCommand.Parameters.Add("@userId", value.OwnerId);
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        // DELETE: api/Subject/5
        // Delete a subject from the database
        [Authorize]
        public void Delete(int id)
        {
            //Get User ID and form SQL Comand
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            string strSql = "DELETE FROM Subject WHERE Id = @SubjectId AND User_ID = @UserId";

            //If user is admin list all repositories no matter what user is the owner
            if (HttpContext.Current.User.IsInRole("Administrator"))
                strSql = "DELETE FROM Subject WHERE Id = @RepositoryId";

            //Open MSSQL Connection and delete data
            _databaseConnection.Open();
            using (_databaseConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(strSql, _databaseConnection))
                {
                    sqlCommand.Parameters.Add("@SubjectId", id);
                    sqlCommand.Parameters.Add("@UserId", userId);
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}