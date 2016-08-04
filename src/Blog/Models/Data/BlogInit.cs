using Blog.Models.Account;
using Blog.Models.PostViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models.Data
{
    sealed public class BlogInit
    {
        private UserManager<BlogUser> userManager { get; }

        public BlogInit(BlogContext context, UserManager<BlogUser> userManager)
        {
            this.userManager = userManager;
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
                    EmailConfirmed = true
                };

                var result = await userManager
                    .CreateAsync(adminUser, "+Vd245aasDR4912Fn+");

                if(!result.Succeeded)
                {
                    throw new InvalidProgramException("Failed to create new user");
                }

            }
         
        }

        public async Task SeedDataAsync()
        {
            await SeedControlUsersAsync();
        }

    }

}

