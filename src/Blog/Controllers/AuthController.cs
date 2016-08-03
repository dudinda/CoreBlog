using Blog.Models;
using Blog.Models.Account;
using Blog.Models.Data;
using Blog.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<BlogUser> _signInManager { get; }

        public AuthController(SignInManager<BlogUser> signInManager)
        {
            _signInManager = signInManager;
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
            if(ModelState.IsValid)
            {

                var user = ModelFactory.Create(viewModel);

                var signInResult = await _signInManager
                    .PasswordSignInAsync( user.UserName, 
                                          user.Password,
                                          true, 
                                          false );

                if ( signInResult.Succeeded )
                {
                    return RedirectToAction("Index", "Blog");
                }
                else
                {
                    ModelState.AddModelError("", "Username or password incorrect");
                }

            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            if(User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }
            return RedirectToActionPermanent("Index", "Blog");
        }
    }
}
