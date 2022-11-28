using Redemption.Domain;
using Redemption.Models;
using Redemption.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redemption.Interfaces
{
    public interface IAuthService
    {
        Task<AuthServiceResponse> RegisterAsync(UserRegVM model);
        Task<AuthServiceResponse> LoginAsync(AuthVM model);
        Task LogoutAsync();
        List<UserList> GetUsers();
    }
}
