using Microsoft.AspNetCore.Identity;

namespace LibraryBookInventory.Infrastructure.Data
{
    public class DbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitializeAsync()
        {
            string[] roles = { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var adminEmail = "admin@library.com";
            var userEmail = "user@library.com";

            var adminUser = await _userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail };
                await _userManager.CreateAsync(adminUser, "Admin@123*");
                await _userManager.AddToRoleAsync(adminUser, "Admin");
            }

            var normalUser = await _userManager.FindByEmailAsync(userEmail);
            if (normalUser == null)
            {
                normalUser = new IdentityUser { UserName = userEmail, Email = userEmail };
                await _userManager.CreateAsync(normalUser, "User@123*");
                await _userManager.AddToRoleAsync(normalUser, "User");
            }
        }
    }
}
