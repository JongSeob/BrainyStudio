using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using Sdk.Models;
using System.Configuration;

namespace WPFapp.Helpers
{
    public class ApiConnection
    {
        RestClient _client = new RestClient();
        RestRequest _request = new RestRequest();

        internal void Request(string v, object gET)
        {
            throw new NotImplementedException();
        }

        public ApiConnection()
        {
            
        }

        public ApiConnection(string url, string login, string password)
        {
            _client = new RestClient(url);
            _client.Authenticator = new HttpBasicAuthenticator(login, password);
        }

        public string Request(string url, Method method)
        {
            _request = new RestRequest(url, Method.GET);
            _request.AddHeader("api_key", "mytestappkey");
            IRestResponse response = _client.Execute(_request);
            return response.Content;
        }
    }
}
