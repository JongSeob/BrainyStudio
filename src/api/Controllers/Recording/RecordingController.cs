using api.Helpers;
using Sdk.Models;
using System;
using System.Collections.Generic;
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
            // Recieve the file and save it to the temporary folder
            var request = HttpContext.Current.Request;
            var filePath = "E:/temp_" + DateTime.Now.Millisecond.ToString();
            using (var fs = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
            {
                request.InputStream.CopyTo(fs);
            }

            // ASYNC:

            // Form a metadata and input them into database


            // Move file into particular folder


            // Delete temporary data
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
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