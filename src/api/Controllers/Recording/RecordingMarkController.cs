using System.Collections.Generic;
using System.Web.Http;

namespace api.Controllers.Recording
{
    public class RecordingMarkController : ApiController
    {
        // GET: api/RecordingMark
        [Authorize]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/RecordingMark/5
        [Authorize]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/RecordingMark
        [Authorize]
        public void Post([FromBody]string value)
        {
        }

        // DELETE: api/RecordingMark/5
        [Authorize]
        public void Delete(int id)
        {
        }
    }
}