using Microsoft.AspNetCore.Identity;

namespace Redemption.Models
{
    public class AuthServiceResponse
    {
        public bool IsValid { get; set; }
        public IEnumerable<IdentityError>? ModelErrors { get; set; }

    }
}
