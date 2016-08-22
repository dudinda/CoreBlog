using Microsoft.AspNetCore.Mvc;
using Blog.Models.Data;
using Blog.Service;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Blog.Models.AccountViewModels;

namespace Blog.Controllers
{
    [ResponseCache(CacheProfileName = "Default")]
    public sealed class BlogController : Controller
    {
        private IMailService mailService { get; }
        private IPageService pageService { get; }
        private IPostService postService { get; } 

        public BlogController (IPostService postService, 
                               IPageService pageService,
                               IMailService mailService)
        {
            this.mailService = mailService;
            this.postService = postService;
            this.pageService = pageService;
        }


        public IActionResult Index()
        {
           return LocalRedirectPermanent("/Blog/1");
        }


        [HttpGet("[controller]/{page:int?}")]
        public IActionResult Index(int page = 1)
        {
               
            var posts       = postService.GetAll();
            var latestposts = postService.GetLatest(posts, 5);
            var pagedList   = pageService.GetPagedList(posts, page);

            //get a counter for each category
            ViewData["Development"] = postService.GetCategoryCounter(posts, "Development");
            ViewData["Managment"]   = postService.GetCategoryCounter(posts, "Managment");
            ViewData["Design"]      = postService.GetCategoryCounter(posts, "Design");
            ViewData["Other"]       = postService.GetCategoryCounter(posts, "Other");

            //get latest posts
            ViewBag.Latest = ModelFactory.Create<PostViewModel>( latestposts );

            var pageViewModel = ModelFactory.Create( pagedList );        
                  
            return View(pageViewModel);
        }

      
        [HttpGet]
        [Route("blog/contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        [HttpPost("[controller]/contact")]
        public IActionResult Contact(ContactViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                mailService.SendEmail(viewModel);

                ModelState.Clear();
                ViewBag.Message = "Thanks for your feedback!";              
            }
            return View();
        }
    }
}
