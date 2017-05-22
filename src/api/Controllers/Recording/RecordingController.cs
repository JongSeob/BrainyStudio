using api.Helpers;
using Sdk.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            // Open connection
            // Recieve the file and save it to the temporary folder
            var request = HttpContext.Current.Request;
            var filePath = "/recordings/" + request.Headers["filename"];
            using (var fs = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
            {
                request.InputStream.CopyTo(fs);
            }

            // Close connection

            // ASYNC:

            // If compressed unzip the temporary file first

            // Read a first 100kb of the file to get metadata

            // Form a metadata and input them into database

            // Move file into particular folder

            // Delete temporary data
        }

        // PUT: api/Record/5
        public void Put(int id, [FromBody]Recording value)
        {
        }

        // DELETE: api/Record/5
        public void Delete(int id)
        {
        }
    }
}