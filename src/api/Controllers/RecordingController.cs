using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using api;
using System.Data.SqlClient;
using api.Helpers;
using Sdk.Models;

namespace api.Controllers
{
    public class RecordingController : ApiController
    {

        DatabaseHelper connection = new DatabaseHelper();
        SerializationHelper s = new SerializationHelper();

        // GET: api/Record
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Record/5
        public Recording Get(int id)
        {
            Recording test = new Recording("Test", DateTime.Now);
            test.Name = "hey";
            return test;
        }

        // POST: api/Record
        public void Post([FromBody]Recording value)
        {
            SqlConnection myConnection = new SqlConnection(connection.ConnString());

            string test = s.Ser<Raw>(value.RawData);
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
