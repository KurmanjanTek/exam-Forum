using Forum.Models;
using Microsoft.AspNetCore.Identity;

namespace Forum.Services;

public class AdminInitializer
{
    public static async Task SeedAdminAndRoleData(RoleManager<IdentityRole<int>> _roleManager, UserManager<User> _userManager)
    {
        string adminEmail = "Admin@admin.admin";
        string adminPassword = "1mag@WSX";
        var roles = new[] { "admin", "user" };
        foreach (var role in roles)
        {
            if (await _roleManager.FindByNameAsync(role) is null)
                await _roleManager.CreateAsync(new IdentityRole<int>(role));
        }

        if (await _userManager.FindByEmailAsync(adminEmail) == null)
        {
            User admin = new User() { Email = adminEmail, UserName = adminEmail, Avatar="/images/avatars/default.png" };
            IdentityResult result = await _userManager.CreateAsync(admin, adminPassword);
            if (result.Succeeded)
                await _userManager.AddToRoleAsync(admin, "admin");
        }
    }
}