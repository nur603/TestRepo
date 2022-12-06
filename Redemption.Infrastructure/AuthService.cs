using Microsoft.AspNetCore.Identity;
using Redemption.Domain;
using Redemption.Interfaces;
using Redemption.Models;
using Redemption.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redemption.Infrastructure
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RedemptionContext _redemptionContext;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager,RedemptionContext redemptionContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _redemptionContext = redemptionContext;
        }

        public List<UserList> GetUsers()
        {
            var result = _redemptionContext.Users.Where(s=>s.LockoutEnd == null).ToList();
            var users = new List<UserList>();
            foreach (var user in result)
            {
                users.Add(new UserList
                {
                    Email = user.Email,
                    Name = user.Name,
                    LastName = user.LastName
                });
            }
            return users;
        }

        public async Task<AuthServiceResponse> LoginAsync(AuthVM model)
        {
            User user = await _userManager.FindByEmailAsync(model.Email);
            if (user is not null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return new AuthServiceResponse
                    {
                        IsValid = true
                    };
                }
            }
            return new AuthServiceResponse
            {
                IsValid = false
            };

        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<AuthServiceResponse> RegisterAsync(UserRegVM model)
        {
            User user = new User
            {
                Email = model.Email.ToLower(),
                Name = model.Name,
                LastName = model.LastName,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            { 
                return new AuthServiceResponse
                {
                    IsValid = true
                };
            }
            else
            {
                return new AuthServiceResponse
                {
                    IsValid = false,
                    ModelErrors = result.Errors
                };
            }

        }
        private int GeneratorAccountNumber()
        {
            Random rnd = new Random();
            var number = 0;
            do
            {
                number = rnd.Next(100000, 999999);
            } while (_userManager.Users.Any(w => w.UserName == number.ToString()));

            return number;
        }

    }
}
