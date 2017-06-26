using api.Helpers;
using api.Security;
using Sdk.Models;
using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace api.MessageHandlers
{
    public class AuthHandler : DelegatingHandler
    {
        ///Database configuration and connection
        private static DatabaseHelper _databaseConfig = new DatabaseHelper();

        private SqlConnection _databaseConnection = new SqlConnection(_databaseConfig.ConnString());
        private User _userId;

        //Method to validate credentials from Authorization
        //header value
        private bool ValidateCredentials(AuthenticationHeaderValue authenticationHeaderVal)
        {
            try
            {
                if (authenticationHeaderVal != null
                    && !String.IsNullOrEmpty(authenticationHeaderVal.Parameter))
                {
                    string[] decodedCredentials
                    = Encoding.ASCII.GetString(Convert.FromBase64String(
                    authenticationHeaderVal.Parameter))
                    .Split(new[] { ':' });

                    //now decodedCredentials[0] will contain
                    //username and decodedCredentials[1] will
                    //contain password.

                    User userId = _databaseConfig.GetUserFromHeader(authenticationHeaderVal);

                    if (userId != null)
                    {
                        _userId = userId;
                        return true; //request authenticated.
                    }
                }
                return false; //request not authenticated.
            }
            catch
            {
                return false;
            }
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //if the credentials are validated,
            //set CurrentPrincipal and Current.User
            if (ValidateCredentials(request.Headers.Authorization))
            {
                Thread.CurrentPrincipal = new APIPrincipal(_userId);
                HttpContext.Current.User = new APIPrincipal(_userId);
            }

            //Execute base.SendAsync to execute default
            //actions and once it is completed,
            //capture the response object and add
            //WWW-Authenticate header if the request
            //was marked as unauthorized.

            //Allow the request to process further down the pipeline
            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized
                && !response.Headers.Contains("WwwAuthenticate"))
            {
                response.Headers.Add("WwwAuthenticate", "Basic");
            }

            return response;
        }
    }
}