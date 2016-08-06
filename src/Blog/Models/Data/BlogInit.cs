using Blog.Models.Account;
using Blog.Models.PostViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models.Data
{
    sealed public class BlogInit
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
            
            var adminRole     = new IdentityRole(roleName: "Admin");
            var moderatorRole = new IdentityRole(roleName: "Moderator");
            var userRole      = new IdentityRole(roleName: "User");

            var userCreateResult      = await roleManager.CreateAsync(userRole);
            var adminCreateResult     = await roleManager.CreateAsync(adminRole);
            var moderatorCreateResult = await roleManager.CreateAsync(moderatorRole);

            //TODO make it with Task.WhenAll()

            //when all tasks is completed
     /*       var result = await Task.WhenAll(userCreateResult,
                                           adminCreateResult,
                                           moderatorCreateResult);


            //check if any task is failed
            /*if (result.Any(task => !task.Succeeded))
            {
                //if it so, throw new exception
                throw new InvalidProgramException("Failed to create new role");
            }*/
         
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
                    throw new InvalidProgramException("Failed to create new user");
                }
                                                         
                var createRoleResult = await userManager
                    .AddToRoleAsync(adminUser, "Admin");

                if(!createRoleResult.Succeeded)
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

