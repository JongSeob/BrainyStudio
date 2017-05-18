using api.Helpers;
using Sdk.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public void Post([FromBody]Recording value)
        {
          
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