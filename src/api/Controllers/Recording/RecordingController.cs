using api.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Http;

namespace api.Controllers
{
    public class RecordingController : ApiController
    {
        ///Database configuration and connection
        private static DatabaseHelper _databaseConfig = new DatabaseHelper();

        private SqlConnection _databaseConnection = new SqlConnection(_databaseConfig.ConnString());

        // GET: api/Record
        // Get all of logged users recordings
        [Authorize]
        public IEnumerable<Sdk.Models.Recording> Get()
        {
            //Get User ID and form SQL Comand
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            string strSql = "SELECT * FROM Recording WHERE User_ID = @userId";

            //If user is admin list all repositories no matter what user is the owner
            if (HttpContext.Current.User.IsInRole("Administrator"))
            {
                strSql = "SELECT * FROM Recording";
            }

            //Open MSSQL Connection and obtain data
            _databaseConnection.Open();
            List<Sdk.Models.Recording> resultsRecording = new List<Sdk.Models.Recording>();
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

        // GET: api/Record/5
        // Get particular recording
        [Authorize]
        public Sdk.Models.Recording Get(int id)
        {
            //Get User ID and form SQL Comand
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            string strSql = "SELECT * FROM Recording WHERE Id = @recordingId AND User_ID = @userId";

            //If user is admin list all repositories no matter what user is the owner
            if (HttpContext.Current.User.IsInRole("Administrator"))
            {
                strSql = "SELECT * FROM Recording WHERE Id = @recordingId";
            }

            //Open MSSQL Connection and obtain data
            _databaseConnection.Open();
            Sdk.Models.Recording resultRecording = new Sdk.Models.Recording();
            using (_databaseConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(strSql, _databaseConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@userId", userId);
                    sqlCommand.Parameters.AddWithValue("@recordingId", id);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            resultRecording.Id = Convert.ToInt32(reader["ID"]);
                            resultRecording.OwnerId = Convert.ToInt32(reader["Owner_ID"]);
                            resultRecording.RepositoryId = Convert.ToInt32(reader["Repository_ID"]);
                            resultRecording.Name = reader["Name"].ToString();
                            resultRecording.Description = reader["Description"].ToString();
                            resultRecording.Date = DateTime.Parse(reader["Date"].ToString());
                            resultRecording.SubjectId = Convert.ToInt32(reader["Subject_ID"]);
                        }
                    }
                }
                return resultRecording;
            }
        }

        // POST: api/Record
        // Upload a new recoridng file
        [Authorize]
        public void Post()
        {
            // TODO: I know its pretty bad - This is just a temporary solution !
            StreamReader reader = new StreamReader(HttpContext.Current.Request.InputStream);
            string requestFromPost = reader.ReadToEnd();
            Sdk.Models.Recording value = JsonConvert.DeserializeObject<Sdk.Models.Recording>(requestFromPost);

            //Get User ID from request
            value.OwnerId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            value.RepositoryId = 1;

            // Form a filename for the data storage
            string filename = "recordings/rec_" + DateTime.UtcNow.ToFileTimeUtc();

            //Form SQL Comand
            String strSql = "INSERT INTO [Recording] (Repository_ID,Owner_ID,Name,Description,Date,Subject_ID,Data_ID) VALUES (@repositoryId,@ownerId,@name,@description,@date,@subjectId,@fileName)";

            // Open connection and save metadata to database
            _databaseConnection.Open();
            using (_databaseConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(strSql, _databaseConnection))
                {
                    sqlCommand.Parameters.Add("@repositoryId", value.RepositoryId);
                    sqlCommand.Parameters.Add("@ownerId", value.OwnerId);
                    sqlCommand.Parameters.Add("@name", value.Name);
                    sqlCommand.Parameters.Add("@description", value.Description);
                    sqlCommand.Parameters.Add("@date", value.Date);
                    sqlCommand.Parameters.Add("@subjectId", value.SubjectId);
                    sqlCommand.Parameters.Add("@fileName", 1); // TODO: Fix filename in database

                    sqlCommand.ExecuteNonQuery();
                }
            }

            // Create a data file to write data.
            File.WriteAllText(filename, requestFromPost);
        }

        // PUT: api/Record/5
        // Edit recording metadata
        [Authorize]
        public void Put(int id, [FromBody]Sdk.Models.Recording value)
        {
            //Get User ID and form SQL Comand
            value.OwnerId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            String strSql = "UPDATE [Recording] SET Repository_ID = @repositoryId, Name = @name, Description = @description , Date = @date, Subject_ID = @SubjectId WHERE Id = @repositoryId AND Owner_ID = @ownerId;";

            //Open connection and update data
            _databaseConnection.Open();
            using (_databaseConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(strSql, _databaseConnection))
                {
                    sqlCommand.Parameters.Add("@recordingId", id);
                    sqlCommand.Parameters.Add("@ownerId", id);
                    sqlCommand.Parameters.Add("@repositoryId", value.RepositoryId);
                    sqlCommand.Parameters.Add("@name", value.Name);
                    sqlCommand.Parameters.Add("@description", value.Description);
                    sqlCommand.Parameters.Add("@date", value.Date);
                    sqlCommand.Parameters.Add("@subjectId", value.SubjectId);
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        //TODO: Mazani souboru z disku
        // DELETE: api/Record/5
        // Delete recording
        [Authorize]
        public void Delete(int id)
        {
            //Get User ID and form SQL Comand
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            string strSql = "DELETE FROM Recording WHERE Id = @RecordingId AND User_ID = @UserId";

            //If user is admin list all repositories no matter what user is the owner
            if (HttpContext.Current.User.IsInRole("Administrator"))
                strSql = "DELETE FROM Recording WHERE Id = @RecordingId";

            //Open MSSQL Connection and delete data
            _databaseConnection.Open();
            using (_databaseConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(strSql, _databaseConnection))
                {
                    sqlCommand.Parameters.Add("@RecordingId", id);
                    sqlCommand.Parameters.Add("@UserId", userId);
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}