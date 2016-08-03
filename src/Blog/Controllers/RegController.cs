using Blog.Models;
using Blog.Models.Account;
using Blog.Models.Data;
using Blog.Models.PostViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class RegController : Controller
    {
        private  UserManager<BlogUser> _userManager { get; }
        private  BlogContext _context { get;}

        public RegController(BlogContext context, UserManager<BlogUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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

                var userExist = await _userManager
                    .FindByNameAsync(viewModel.Name);

                if (userExist != null)
                {
                    ModelState.AddModelError("", $"Username {viewModel.Name} already exists");
                    return View();
                }

                var emailExist = await _userManager
                    .FindByEmailAsync(viewModel.Email);

                if (emailExist != null)
                {
                    ModelState.AddModelError("", $"Username with {viewModel.Email} email already exists");
                    return View();
                }


                var newUser = ModelFactory.Create(viewModel);

                var result = await _userManager
                    .CreateAsync(newUser, viewModel.Password);
               
                if (result.Succeeded)
                {
                    _context.SaveChanges();
                    return RedirectToActionPermanent("Index", "Blog");
                }
            }

            return View(viewModel);
        }
    }
}
