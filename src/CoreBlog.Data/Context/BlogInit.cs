using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBlog.Data.Context
{
    public sealed class BlogInit
    {
        private RoleManager<IdentityRole> roleManager { get; }
        private UserManager<BlogUser> userManager { get; }

        public BlogInit(UserManager<BlogUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }


        private async Task SeedRolesAsync()
        {
            if (!roleManager.Roles.Any()) {

                var adminRole  = new IdentityRole("Admin");
                var userRole   = new IdentityRole("User");
                var bannedRole = new IdentityRole("Banned");

                var userCreateResult = await roleManager.CreateAsync(userRole);

                if(!userCreateResult.Succeeded)
                {
                    throw new InvalidProgramException("Failed to create new role");
                }

                var adminCreateResult = await roleManager.CreateAsync(adminRole);

                if (!adminCreateResult.Succeeded)
                {
                    throw new InvalidProgramException("Failed to create new role");
                }

                var bannedCreateResult = await roleManager.CreateAsync(bannedRole);

                if (!bannedCreateResult.Succeeded)
                {
                    throw new InvalidProgramException("Failed to create new role");
                }

            }
        }

        private async Task SeedControlUsersAsync()
        {
            var userExist = await userManager
                .FindByNameAsync("admin");

            if (userExist == null)
            {
                var adminUser = new BlogUser()
                {
                    UserName = "admin",
                    Email = "enragesoft@gmail.com",
                    EmailConfirmed = true,
                    
                };

                var createUserResult = await userManager
                    .CreateAsync(adminUser, "+Vd245aasDR4912Fn+");

                if (!createUserResult.Succeeded)
                {
                    throw new InvalidProgramException("Failed to create user role");
                }
                                                         
                var createAdminResult = await userManager
                    .AddToRoleAsync(adminUser, "Admin");

                if(!createAdminResult.Succeeded)
                {
                    throw new InvalidProgramException("Failed to create admin role");
                }  

            }
         
        }

        public async Task SeedDataAsync()
        {
            await SeedRolesAsync();
            await SeedControlUsersAsync();
        }

    }

}

