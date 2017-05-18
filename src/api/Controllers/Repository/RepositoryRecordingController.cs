using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api.Controllers
{
    public class RepositoryRecordingController : ApiController
    {
        // GET: api/RepositoryRecording
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/RepositoryRecording/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/RepositoryRecording
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/RepositoryRecording/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/RepositoryRecording/5
        public void Delete(int id)
        {
        }
    }
}
