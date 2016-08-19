using Blog.Models.Account;
using Blog.Models.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [ResponseCache(CacheProfileName = "Default")]
    public sealed partial class AdminController : Controller
    {

        private UserManager<BlogUser> userManager { get; }
        private IPostService postService { get; }

        public AdminController(IPostService postService, UserManager<BlogUser> userManager)
        {
            this.userManager = userManager;
            this.postService = postService;
        }

        public IActionResult Manage()
        {
            return View();
        }

    }
}
