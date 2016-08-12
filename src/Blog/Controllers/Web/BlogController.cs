using System.Linq;
using Blog.Models.PostViewModels;
using Microsoft.AspNetCore.Mvc;
using Blog.Models.Data;
using Blog.Service;
using Blog.Models.Factory;
using Blog.ViewModels;

namespace Blog.Controllers
{
    public sealed class BlogController : Controller
    {
        private IPageService pageService { get; }
        private IPostService postService { get; } 

        public BlogController (IPostService repository, IPageService pageService)
        {
            this.postService = repository;
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
            var latestposts = postService.GetLatest(ref posts, 5);
            var pagedList   = pageService.GetPagedList(posts, page);

            //get a counter for each categoty
            ViewData["Development"] = posts.Where(option => option.Category.Name == "Development").Count();
            ViewData["Managment"]   = posts.Where(option => option.Category.Name == "Managment").Count();
            ViewData["Design"]      = posts.Where(option => option.Category.Name == "Design").Count();
            ViewData["Other"]       = posts.Where(option => option.Category.Name == "Other").Count();

            //get latest posts
            ViewData["Latest"] = ModelFactory.Create<PostViewModel>( latestposts );

            var pageViewModel = ModelFactory.Create( pagedList );        
                  
            return View(pageViewModel);
        }

      

        public IActionResult Contact()
        {
            return View();
        }


        [HttpPost("[controller]/contact")]
        public IActionResult Contact(ContactViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var name = viewModel.Name;
                return RedirectToActionPermanent("SuccessContact", viewModel);
            }

            return View();
        }


    }
}
