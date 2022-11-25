using Microsoft.AspNetCore.Identity;
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

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                
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
    }
}
