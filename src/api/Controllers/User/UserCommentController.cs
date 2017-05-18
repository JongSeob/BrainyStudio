using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api.Controllers
{
    public class UserCommentController : ApiController
    {
        // GET: api/UserComment
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/UserComment/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/UserComment
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/UserComment/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/UserComment/5
        public void Delete(int id)
        {
        }
    }
}
