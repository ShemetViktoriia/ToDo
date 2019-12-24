using Microsoft.AspNet.Identity.EntityFramework;

namespace ToDo_DAL
{
    public partial class AspNetUser : IdentityUser<int, AspNetUserLogin, AspNetUserRole, AspNetUserClaim>
    {

    }
}
