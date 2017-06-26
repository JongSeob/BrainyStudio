using Sdk.Models;
using System.Security.Principal;

namespace api.Security
{
    public class APIPrincipal : IPrincipal
    {
        public User AuthUser { get; set; }

        //Constructor
        public APIPrincipal(User auth)
        {
            AuthUser = auth;
            Identity = new GenericIdentity(AuthUser.Id.ToString());
        }

        public IIdentity Identity { get; set; }

        public bool IsInRole(string role)
        {
            return role.Equals(AuthUser.Role);
        }
    }
}