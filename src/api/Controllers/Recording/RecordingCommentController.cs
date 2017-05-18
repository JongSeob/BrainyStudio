using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api.Controllers
{
    public class RecordingCommentController : ApiController
    {
        // GET: api/RecordingComment
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/RecordingComment/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/RecordingComment
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/RecordingComment/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/RecordingComment/5
        public void Delete(int id)
        {
        }
    }
}
