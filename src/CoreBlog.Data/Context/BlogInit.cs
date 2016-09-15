using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBlog.Data.Context
{
    public sealed class BlogInit
    {
        private IConfigurationRoot config { get; }
        private RoleManager<IdentityRole> roleManager { get; }
        private UserManager<BlogUser> userManager { get; }

        public BlogInit(UserManager<BlogUser> userManager,
                        RoleManager<IdentityRole> roleManager,
                        IConfigurationRoot config)  
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.config = config;
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
                .FindByNameAsync(config["Site:Username"]);

            if (userExist == null)
            {
                var adminUser = new BlogUser()
                {
                    UserName       = config["Site:Username"],
                    Email          = config["Site:Email"],
                    EmailConfirmed = true,
                    
                };

                var createUserResult = await userManager
                    .CreateAsync(adminUser, config["Site:Password"]);

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

