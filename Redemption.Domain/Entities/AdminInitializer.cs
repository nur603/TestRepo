using Microsoft.AspNetCore.Identity;

namespace Redemption.Models.Data
{
    public class AdminInitializer
    {
        public static async Task SeedAdminUser(
            RoleManager<IdentityRole> _roleManager,
            UserManager<User> _userManager)
        {
            string adminEmail = "admin@yurta.me";
            string adminPassword = "Q1w2e3r4t5y@";

            var roles = new[] { "admin", "user" };

            foreach (var role in roles)
            {
                if (await _roleManager.FindByNameAsync(role) is null)
                    await _roleManager.CreateAsync(new IdentityRole(role));
            }
            if (await _userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail, Name = "Admin"};
                IdentityResult result = await _userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                    await _userManager.AddToRoleAsync(admin, "admin");
            }
        }
    }
}
