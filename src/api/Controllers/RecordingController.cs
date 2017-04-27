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
        private DatabaseHelper connection = new DatabaseHelper();
        private SerializationHelper s = new SerializationHelper();

        // GET: api/Record
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Record/5
        public Recording Get(int id)
        {
            Recording test = new Recording("Test", DateTime.Now);
            test._name = "hey";
            return test;
        }

        // POST: api/Record
        public void Post([FromBody]Recording value)
        {
            SqlConnection myConnection = new SqlConnection(connection.ConnString());

            string test = s.Ser<Raw>(value._raw);
            Raw test2 = s.Deser<Raw>(test);
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