using api.Helpers;
using Sdk.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;

namespace api.Controllers
{
    public class RecordingController : ApiController
    {
        ///Database configuration and connection
        private static DatabaseHelper _databaseConfig = new DatabaseHelper();
        private SqlConnection _databaseConnection = new SqlConnection(_databaseConfig.ConnString());

        // GET: api/Record
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Record/5
        public Recording Get(int id)
        {
            return null;
        }


        // POST: api/Record
        public void Post()
        {
            // TODO: I know its pretty bad - This is just a temporary solution !
            StreamReader reader = new StreamReader(HttpContext.Current.Request.InputStream);
            string requestFromPost = reader.ReadToEnd();
            Recording value = JsonConvert.DeserializeObject<Recording>(requestFromPost);

            //Get User ID from request
            value.OwnerId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            value.RepositoryId = 1;
            // Form a filename for the data storage
            string filename = "recording_" + DateTime.UtcNow.ToFileTimeUtc();

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

        // TODO
        // PUT: api/Record/5
        public void Put(int id, [FromBody]Recording value)
        {
        }

        // DELETE: api/Record/5
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