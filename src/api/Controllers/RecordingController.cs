using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using api.Models;
using Newtonsoft.Json;
using api;
using System.Data.SqlClient;
using api.Helpers;

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
        public EEGRecording Get(int id)
        {
            EEGRecording test = new EEGRecording(1, "ppeik");
            test.Name = "hey";
            return test;
        }

        // POST: api/Record
        public void Post([FromBody]EEGRecording value)
        {
            SqlConnection myConnection = new SqlConnection(connection.ConnString());

            string test = s.Ser<List<Double>>(value.F3);
            List<Double> test2 = s.Deser<List<Double>>(test);
        }

        // PUT: api/Record/5
        public void Put(int id, [FromBody]EEGRecording value)
        {
        }

        // DELETE: api/Record/5
        public void Delete(int id)
        {
        }
    }
}
