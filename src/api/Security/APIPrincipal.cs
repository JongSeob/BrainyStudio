using System.Security.Principal;
using Sdk.Models;

namespace api.Security
{
    public class APIPrincipal : IPrincipal
    {
        public User AuthUser {get; set; }

        //Constructor
        public APIPrincipal(User Auth)
        {
            AuthUser = Auth;
            Identity = new GenericIdentity(AuthUser._id.ToString());
        }

        public IIdentity Identity { get; set; }

        public bool IsInRole(string role)
        {
            if (role.Equals(AuthUser._Role))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}