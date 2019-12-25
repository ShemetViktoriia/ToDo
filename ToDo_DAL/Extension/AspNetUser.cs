using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ToDo_DAL
{
    public partial class AspNetUser : IUser<int>
    {
        public ClaimsIdentity GenerateUserIdentity(UserManager<AspNetUser, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = manager.CreateIdentity(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AspNetUser, int> manager)
        {
            return Task.FromResult(GenerateUserIdentity(manager));
        }
    }
}
