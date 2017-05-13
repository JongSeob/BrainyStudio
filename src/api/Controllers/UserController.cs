using api.Helpers;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Http;

namespace api.Controllers
{
    public class UserController : ApiController
    {

        /// Global helpers for Database configuration, connection and data serialization
        private static DatabaseHelper _DBconfig = new DatabaseHelper();

        private SqlConnection _myConnection = new SqlConnection(_DBconfig.ConnString());
        private SerializationHelper _serializer = new SerializationHelper();


        // GET: api/User
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/User/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}