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

            
            var user1 = await userManager.FindByEmailAsync("abdullahbdreldin@gmail.com");
            if (user1 == null)
            {
                user1 = new User
                {
                    DisplayName = "Abdullah",
                    Email = "abdullahbdreldin@gmail.com",
                    UserName = "abdullahbdreldin",
                    Address = new Address
                    {
                        FirstName = "Abdullah",
                        LastName = "Bdreldin",
                        City = "Alexandria",
                        Street = "Abu Qir",
                        State = "Egypt",
                        ZipCode = "123456"
                    }
                };

                var result = await userManager.CreateAsync(user1, "Password@123");
            }

            
            var user2 = await userManager.FindByEmailAsync("secondadmin@example.com");
            if (user2 == null)
            {
                user2 = new User
                {
                    DisplayName = "Second Admin",
                    Email = "secondadmin@example.com",
                    UserName = "secondadmin",
                    Address = new Address
                    {
                        FirstName = "Second",
                        LastName = "Admin",
                        City = "Cairo",
                        Street = "Maadi",
                        State = "Egypt",
                        ZipCode = "654321"
                    }
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
