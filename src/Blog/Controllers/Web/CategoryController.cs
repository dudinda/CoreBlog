using Blog.Models.Data;
using Blog.Service;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("[controller]/")]
    sealed public class CategoryController : Controller
    {
        private IPageService pageService { get; }
        private IPostService postService { get; }

        public CategoryController(IPostService postService, IPageService pageService)
        {
            this.postService = postService;
            this.pageService = pageService;
        }

        [HttpPost]
        public IActionResult GetString(SearchViewModel search)
        {
            if (ModelState.IsValid)
            {
                return RedirectPermanent($"Category/{search.Text}");
            }

            return BadRequest();
        }

    
        [HttpGet("{text}/{page:int?}")]
        public IActionResult SearchCategory(string text, int page = 1)
        {
            var posts     = postService.GetPostsByCategory(text);
            var pagedList = pageService.GetPagedList(posts, page);

            ViewBag.Controller = "Category";

            var pageViewModel = ModelFactory.Create(pagedList);

            return View("../Search/SearchResult", pageViewModel);
        }
    }
}
