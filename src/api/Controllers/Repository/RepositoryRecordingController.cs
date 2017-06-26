using api.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Http;

namespace api.Controllers
{
    public class RepositoryRecordingController : ApiController
    {
        ///Database configuration and connection
        private static DatabaseHelper _databaseConfig = new DatabaseHelper();

        private SqlConnection _databaseConnection = new SqlConnection(_databaseConfig.ConnString());

        // Get all recordings metadata from the repository
        // GET: api/RepositoryRecording
        [Authorize]
        public IEnumerable<Sdk.Models.Recording> Get(int id)
        {
            //Get User ID and form SQL Comand
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            string strSql = "SELECT * FROM Recording WHERE User_ID = @userId AND Repository_ID = @repositoryId";

            //If user is admin list all repositories no matter what user is the owner
            if (HttpContext.Current.User.IsInRole("Administrator"))
            {
                strSql = "SELECT * FROM Recording WHERE Repository_ID = @repositoryId";
            }

            //Open MSSQL Connection and obtain data
            _databaseConnection.Open();
            List<Sdk.Models.Recording> resultsRecording = new List<Sdk.Models.Recording>();
            using (_databaseConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(strSql, _databaseConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@userId", userId);
                    sqlCommand.Parameters.AddWithValue("@repositoryId", id);
                    SqlDataAdapter parser = new SqlDataAdapter(sqlCommand);
                    DataTable datatable = new DataTable();
                    parser.Fill(datatable);

                    foreach (DataRow row in datatable.Rows)
                    {
                        Sdk.Models.Recording resultRecording = new Sdk.Models.Recording();
                        resultRecording.Id = Convert.ToInt32(row["ID"]);
                        resultRecording.OwnerId = Convert.ToInt32(row["Owner_ID"]);
                        resultRecording.RepositoryId = Convert.ToInt32(row["Repository_ID"]);
                        resultRecording.Name = row["Name"].ToString();
                        resultRecording.Description = row["Description"].ToString();
                        resultRecording.Date = DateTime.Parse(row["Date"].ToString());
                        resultRecording.SubjectId = Convert.ToInt32(row["Subject_ID"]);
                    }
                }
                return resultsRecording;
            }
        }
    }
}