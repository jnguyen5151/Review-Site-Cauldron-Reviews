using Microsoft.AspNetCore.Identity;
using ReviewAPI.Authorization;

namespace ReviewAPI.Data
{
    public static class RoleSeed
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles =
            {
                Roles.User,
                Roles.Moderator,
                Roles.Admin
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
