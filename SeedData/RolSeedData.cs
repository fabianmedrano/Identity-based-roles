using Identity.Areas.Identity.Data;
using Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.SeedData
{
    public class RolSeedData
    {

        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Admin", "User" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Aquí puedes crear un usuario administrador y asignarle el rol de administrador
            var user = new User
            {
                UserName = "admin",
                Email = "admin@test.com",
            };

            string userPassword = "Password123!";
            var userExist = await userManager.FindByNameAsync(user.UserName);

            if (userExist == null)
            {
                var createPowerUser = await userManager.CreateAsync(user, userPassword);
                if (createPowerUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
        
    }
}
