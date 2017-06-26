using System.Collections.Generic;
using System.Web.Http;

namespace api.Controllers.Recording
{
    public class RecordingDataController : ApiController
    {
        // GET: api/RecordingData
        [Authorize]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}