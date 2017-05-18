using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using System.Web.Http;
using api.Helpers;
using Sdk.Models;

namespace api.Controllers
{
    public class CommentController : ApiController
    {
        ///Database configuration and connection
        private static DatabaseHelper _databaseConfig = new DatabaseHelper();
        private SqlConnection _databaseConnection = new SqlConnection(_databaseConfig.ConnString());


        // GET: api/Comment/5
        [Authorize]
        public Comment Get(int id)
        {
            //Form SQL Comand
            string strSql = "SELECT * FROM Comment WHERE Id = @commentId";

            //Open MSSQL Connection and obtain data
            _databaseConnection.Open();
            Comment resultComment = new Comment();
            using (_databaseConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(strSql, _databaseConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@commentId", id);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            resultComment.Id = Convert.ToInt32(reader["Id"]);
                            resultComment.OwnerId= Convert.ToInt32(reader["User_ID"]);
                            resultComment.RecordingId = Convert.ToInt32(reader["Recording_ID"]);
                            resultComment.Text = reader["Text"].ToString();
                            resultComment.Timestamp = DateTime.Parse(reader["Timestamp"].ToString());
                        }
                    }
                }
                return resultComment;
            }
        }


        // POST: api/Comment
        [Authorize]
        public void Post([FromBody]Comment value)
        {
            //Get User ID and form SQL Comand
            value.OwnerId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            String strSql = "INSERT INTO Comment (User_ID, Recording_ID, Text, Timestamp) VALUES (@userId, @recordingId, @text, @timestamp)";

            // Open connection
            _databaseConnection.Open();
            using (_databaseConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(strSql, _databaseConnection))
                {
                    sqlCommand.Parameters.Add("@userId", value.OwnerId);
                    sqlCommand.Parameters.Add("@recordingId", value.RecordingId);
                    sqlCommand.Parameters.Add("@text", value.Text);
                    sqlCommand.Parameters.Add("@timestamp", value.Timestamp);
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }


        // PUT: api/Comment/5
        [Authorize]
        public void Put(int id, [FromBody]Comment value)
        {
        }


        // DELETE: api/Comment/5
        [Authorize]
        public void Delete(int id)
        {
            //Get User ID and form SQL Comand
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            string strSql = "DELETE FROM Comment WHERE Id = @CommentId AND User_ID = @UserId";

            //If user is admin list all repositories no matter what user is the owner
            if (HttpContext.Current.User.IsInRole("Administrator"))
                strSql = "DELETE FROM Repository WHERE Id = @RepositoryId";

            //Open MSSQL Connection and delete data
            _databaseConnection.Open();
            using (_databaseConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(strSql, _databaseConnection))
                {
                    sqlCommand.Parameters.Add("@CommentId", id);
                    sqlCommand.Parameters.Add("@UserId", userId);
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}