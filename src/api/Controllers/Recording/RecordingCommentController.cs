using api.Helpers;
using Sdk.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Http;

namespace api.Controllers
{
    public class RecordingCommentController : ApiController
    {
        ///Database configuration and connection
        private static DatabaseHelper _databaseConfig = new DatabaseHelper();

        private SqlConnection _databaseConnection = new SqlConnection(_databaseConfig.ConnString());

        // GET: api/RecordingComment
        [Authorize]
        public IEnumerable<Comment> Get(int id)
        {
            //Get User ID and form SQL Comand
            string strSql = "SELECT * FROM Comment WHERE Recording_ID = @recordingId";

            //Open MSSQL Connection and obtain data
            _databaseConnection.Open();
            List<Comment> resultComments = new List<Comment>();
            using (_databaseConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(strSql, _databaseConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@recordingId", id);
                    SqlDataAdapter parser = new SqlDataAdapter(sqlCommand);
                    DataTable datatable = new DataTable();
                    parser.Fill(datatable);

                    foreach (DataRow row in datatable.Rows)
                    {
                        Comment tempComment = new Comment();
                        tempComment.Id = Convert.ToInt32(row["ID"]);
                        tempComment.OwnerId = Convert.ToInt32(row["User_ID"]);
                        tempComment.RecordingId = Convert.ToInt32(row["Recording_ID"]);
                        tempComment.Text = row["Text"].ToString();
                        tempComment.Timestamp = DateTime.Parse(row["Timestamp"].ToString());
                        resultComments.Add(tempComment);
                    }
                }
                return resultComments;
            }
        }

        // POST: api/RecordingComment
        [Authorize]
        public void Post([FromBody]Comment value, int id)
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
                    sqlCommand.Parameters.Add("@recordingId", id);
                    sqlCommand.Parameters.Add("@text", value.Text);
                    sqlCommand.Parameters.Add("@timestamp", value.Timestamp);
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}