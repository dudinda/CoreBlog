using Blog.Models.Account;
using Blog.Models.AccountViewModels;
using Blog.Models.Data;
using Blog.Models.PostViewModels;
using Blog.Service;
using Blog.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [ResponseCache(CacheProfileName = "Default")]
    sealed public class RegController : Controller
    {
        private IMailService mailService { get; }
        private ILogger<RegController> logger { get; }
        private  UserManager<BlogUser> userManager { get; }
        private  BlogContext context { get; }

        public RegController(BlogContext context, 
                             UserManager<BlogUser> userManager,
                             ILogger<RegController> logger,
                             IMailService mailService)
        {
            this.context     = context;
            this.userManager = userManager;
            this.logger      = logger;
            this.mailService = mailService;
        }

   
        [HttpGet("[controller]/registration")]
        public IActionResult Registration()
        {
            return View();
        }


        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail()
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(Request.Query["id"]);

                if (user != null)
                {
                    var result = await userManager.ConfirmEmailAsync(user, Request.Query["token"]);

                    if(result.Succeeded)
                    {
                        return View();
                    }
                }
            }
            return NotFound();
        }

        [HttpPost("[controller]/registration")]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegistrationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {         
                var userExist = await userManager
                    .FindByNameAsync(viewModel.UserName);

                if (userExist != null)
                {
                    ModelState.AddModelError("", $"Username {viewModel.UserName} already exists");
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
                   
                if (result.Succeeded)
                {
                    var userRole = await userManager.AddToRoleAsync(newUser, "User");

                    if (userRole.Succeeded)
                    {
                        context.SaveChanges();

                        logger.LogInformation($"New account: {newUser.UserName}, {newUser.Email}");

                        //generate token
                        var token = await userManager.GenerateEmailConfirmationTokenAsync(newUser);

                        //generate callbackUrl
                        var callback = Url.Action("ConfirmEmail",
                                                  "Reg",
                                                  new { id = newUser.Id, token = token },
                                                  protocol: HttpContext.Request.Scheme);

                        await mailService.ConfirmEmailAsync(newUser, callback);

                        ViewData["Email"]   = newUser.Email;
                        ViewData["Message"] = "Thanks for joining us!";

                        return View();
                    }
                }
            }
            logger.LogError($"Failed to register new user: {viewModel.UserName}, {viewModel.Email}");
            return BadRequest();
        }
    }
}
