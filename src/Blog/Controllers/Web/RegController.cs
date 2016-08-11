using Blog.Models.Account;
using Blog.Models.Data;
using Blog.Models.PostViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    sealed public class RegController : Controller
    {
        private  UserManager<BlogUser> userManager { get; }
        private  BlogContext context { get;}

        public RegController(BlogContext context, UserManager<BlogUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpGet("[controller]/registration")]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost("[controller]/registration")]
     //   [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegistrationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {         

                var userExist = await userManager
                    .FindByNameAsync(viewModel.Name);

                if (userExist != null)
                {
                    ModelState.AddModelError("", $"Username {viewModel.Name} already exists");
                    return View();
                }

                var emailExist = await userManager
                    .FindByEmailAsync(viewModel.Email);

                if (emailExist != null)
                {
                    ModelState.AddModelError("", $"Username with {viewModel.Email} email already exists");
                    return View();
                }


                var newUser = ModelFactory.Create(viewModel);

                var result = await userManager
                    .CreateAsync(newUser, viewModel.Password);


                var userRole = await userManager.AddToRoleAsync(newUser, "User");


                if (!userRole.Succeeded)
                {
                    throw new InvalidProgramException("Failed to bind user to the role");
                }

                if (result.Succeeded)
                {
                    context.SaveChanges();
                    return RedirectToActionPermanent("Index", "Blog");
                }
              
            }

            return View(viewModel);
        }
    }
}
