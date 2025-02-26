using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodayWebAPi.DAL.Data.Context;

namespace TodayWebAPi.DAL.Data.Identity
{
    public class IdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            
            var roleExist = await roleManager.RoleExistsAsync("Admin");
            if (!roleExist)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            var customRoleExist = await roleManager.RoleExistsAsync("customer");
            if (!customRoleExist)
            {
                await roleManager.CreateAsync(new IdentityRole("customer"));
            }

            var user1 = await userManager.FindByEmailAsync("abdullahbdreldin@gmail.com");
            if (user1 == null)
            {
                user1 = new User
                {
                    DisplayName = "Abdullah",
                    Email = "abdullahbdreldin@gmail.com",
                    UserName = "abdullahbdreldin",
                };

                var result = await userManager.CreateAsync(user1, "Password@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user1, "customer");
                }
            }
            else
            {
                var roles = await userManager.GetRolesAsync(user1);
                if (!roles.Contains("customer"))
                {
                    await userManager.AddToRoleAsync(user1, "customer");
                }
            }


            var user2 = await userManager.FindByEmailAsync("secondadmin@example.com");
            if (user2 == null)
            {
                user2 = new User
                {
                    DisplayName = "Second Admin",
                    Email = "secondadmin@example.com",
                    UserName = "secondadmin",
                };

                var result = await userManager.CreateAsync(user2, "Password@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user2, "Admin");
                }
            }
            else
            {
                var roles = await userManager.GetRolesAsync(user2);
                if (!roles.Contains("Admin"))
                {
                    await userManager.AddToRoleAsync(user2, "Admin");
                }
            }
        }
    }
}
