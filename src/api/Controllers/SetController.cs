using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api.Controllers
{
    public class SetController : ApiController
    {
        // GET: api/Set
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Set/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Set
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Set/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Set/5
        public void Delete(int id)
        {
        }
    }
}
