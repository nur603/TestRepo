using Microsoft.AspNetCore.Identity;

namespace Redemption.Models.Data
{
    public class User : IdentityUser
    {
       public string Name { get; set; }
        public string? LastName { get; set; }
    }
}
