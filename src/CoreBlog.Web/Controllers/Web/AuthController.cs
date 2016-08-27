using CoreBlog.Data.Context;
using CoreBlog.Web.Services;
using CoreBlog.Web.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CoreBlog.Web.Controllers
{
    [ResponseCache(CacheProfileName = "Default")]
    sealed public class AuthController : Controller
    {
        private IMailService mailService { get; }
        private ILogger<AuthController> logger { get; }
        private UserManager<BlogUser> userManager { get; }
        private SignInManager<BlogUser> signInManager { get; }

        public AuthController(SignInManager<BlogUser> signInManager,
                              UserManager<BlogUser> userManager,
                              ILogger<AuthController> logger,
                              IMailService mailService)
        {
            this.signInManager = signInManager;
            this.userManager   = userManager;
            this.logger        = logger;
            this.mailService   = mailService;
        }
        #region Login.
        [AllowAnonymous]
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
        #endregion

        #region Logout.
        public async Task<IActionResult> Logout()
        {
            if(User.Identity.IsAuthenticated)
            {            
                await signInManager.SignOutAsync();
                logger.LogInformation($"{User.Identity.Name} is now logged out.");
            }
            return RedirectToActionPermanent("Index", "Blog");
        }
        #endregion


        #region Send reset link.
        [HttpGet]
        public IActionResult SendResetLink()
        {
            return View(new SendResetPasswordViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> SendResetLink(SendResetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                var user = await userManager.FindByEmailAsync(viewModel.Email);

                if (user != null)
                {

                    var isConfirmed = await userManager.IsEmailConfirmedAsync(user);

                    //if email is not confirmed
                    if (!isConfirmed)
                    {
                        ModelState.AddModelError("", "Please confirm your registration email first.");
                        return View();
                    }

                    //generate token
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);

                    //generate callback url
                    var callback = Url.Action("ResetPassword",
                                              "Auth",
                                              new { id = user.Id, token = token },
                                              protocol: HttpContext.Request.Scheme);

                    await mailService.ConfirmEmailAsync(user, callback);

                    ViewData["Message"] = "Thanks! We sent a password reset link to your email address.";
                    return View();
                }
            }

            return BadRequest();
        }
        #endregion

        #region Reset password.
        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View(new ResetPasswordViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                var user = await userManager.FindByIdAsync(Request.Query["id"]);
                //if user exists
                if (user != null)
                {
                    if (user.Email != viewModel.Email)
                    {
                        ModelState.AddModelError("", "Email addresses don't match.");
                        return View();
                    }

                    var result = await userManager.ResetPasswordAsync(user, Request.Query["token"], viewModel.Password);

                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Code);
                        }

                        return View();
                    }

                    return View("Login");
                }
            }

            return NotFound();
        }
        #endregion

        #region Forbidden.
        public IActionResult Forbidden()
        {
            return View();
        }
        #endregion

    }
 
}
