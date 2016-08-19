using Blog.Models.Account;
using Blog.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [ResponseCache(CacheProfileName = "Default")]
    sealed public class AuthController : Controller
    {
        private UserManager<BlogUser> userManager { get; }
        private SignInManager<BlogUser> signInManager { get; }

        public AuthController(SignInManager<BlogUser> signInManager, UserManager<BlogUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager   = userManager;
        }

        public IActionResult Login()
        {
            if(User.Identity.IsAuthenticated)
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
                var user  = await userManager.FindByNameAsync(viewModel.UserName);
                    user  = await userManager.FindByEmailAsync(viewModel.UserName); 

                //if user exist
                if (user != null) {

                    if (user.isBanned)
                    {
                        ModelState.AddModelError("", $"Your account has been temporarily suspended until {user.LockoutEnd}.");
                        return View();
                    }

                    var signInResult = await signInManager
                        .PasswordSignInAsync(user,
                                             viewModel.Password,
                                             viewModel.RememberMe,
                                             false);

                    if (signInResult.Succeeded)
                    {
                        return RedirectToAction("Index", "Blog");
                    }         
                }
            }

            ModelState.AddModelError("", "Login or password is incorrect.");
       
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            if(User.Identity.IsAuthenticated)
            {
                await signInManager.SignOutAsync();
            }
            return RedirectToActionPermanent("Index", "Blog");
        }
    }
}
