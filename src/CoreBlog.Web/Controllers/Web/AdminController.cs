using CoreBlog.Data.Context;
using CoreBlog.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreBlog.Web.Controllers
{
    [ResponseCache(CacheProfileName = "Default")]
    public sealed partial class AdminController : Controller
    {
      
        private UserManager<BlogUser> userManager { get; }
        private IPostService postService { get; }
        private ILogger<AdminController> logger { get; }

        public AdminController(IPostService postService, 
                               UserManager<BlogUser> userManager, 
                               ILogger<AdminController> logger)
        {
            this.userManager = userManager;
            this.postService = postService;
            this.logger      = logger;
        }

        public IActionResult Manage()
        {
            return View();
        }

    }
}
