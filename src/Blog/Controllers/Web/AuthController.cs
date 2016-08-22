using Blog.Models.Account;
using Blog.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [ResponseCache(CacheProfileName = "Default")]
    sealed public class AuthController : Controller
    {
        private ILogger<AuthController> logger { get; }
        private UserManager<BlogUser> userManager { get; }
        private SignInManager<BlogUser> signInManager { get; }

        public AuthController(SignInManager<BlogUser> signInManager,
                              UserManager<BlogUser> userManager,
                              ILogger<AuthController> logger)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.logger = logger;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Blog");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //get an existing user by name or email
                var user = await userManager.FindByNameAsync(viewModel.UserName);
                    user = await userManager.FindByEmailAsync(viewModel.UserName);

                //if user exist
                if (user != null) {

                    if (user.isBanned)
                    {
                        ModelState.AddModelError("", $"Your account has been temporarily suspended until {user.LockoutEnd}.");
                        return View();
                    }

                    if(user.EmailConfirmed == false)
                    {
                        ModelState.AddModelError("", $"You must activate your account with the code sent to your email address: {user.Email}");
                        return View();
                    }

                    var signInResult = await signInManager
                        .PasswordSignInAsync(user,
                                             viewModel.Password,
                                             viewModel.RememberMe,
                                             false);

                    if (signInResult.Succeeded)
                    {
                        logger.LogInformation($"{User.Identity.Name} is now logged in.");
                        return RedirectToAction("Index", "Blog");
                    }
                }
            }
            logger.LogInformation($"Attemp to log in with {viewModel.UserName} and {viewModel.Password} failed.");
            ModelState.AddModelError("", "Login or password is incorrect.");

            return View();
        }


        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }


        public async Task<IActionResult> Logout()
        {
            if(User.Identity.IsAuthenticated)
            {            
                await signInManager.SignOutAsync();
                logger.LogInformation($"{User.Identity.Name} is now logged out.");
            }
            return RedirectToActionPermanent("Index", "Blog");
        }
    }
}
