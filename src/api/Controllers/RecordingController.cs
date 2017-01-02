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

namespace api.Controllers
{
    public class RecordingController : ApiController
    {
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
            DatabaseController connection = new DatabaseController();
            SqlConnection myConnection = new SqlConnection(connection.ConnString());

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
