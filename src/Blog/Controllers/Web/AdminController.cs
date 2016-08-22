using Blog.Models.Account;
using Blog.Models.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blog.Controllers
{
    [ResponseCache(CacheProfileName = "Default")]
    public sealed partial class AdminController : Controller
    {
      
        private UserManager<BlogUser> userManager { get; }
        private IPostService postService { get; }
        private ILogger<AdminController> logger { get; set; }

        public AdminController(IPostService postService, 
                               UserManager<BlogUser> userManager, 
                               ILogger<AdminController> logger)
        {
            this.userManager = userManager;
            this.postService = postService;
            this.logger = logger;
        }

        public IActionResult Manage()
        {
            return View();
        }

    }
}
