using Microsoft.AspNetCore.Mvc;
using CoreBlog.Web.ViewModels.Page;
using CoreBlog.Web.Factory;
using CoreBlog.Web.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreBlog.Web.Controllers
{
    [Route("[controller]/")]
    sealed public class TagController : Controller
    {
        private IPageService pageService { get; }
        private IPostService postService { get; }

        public TagController(IPostService postService, IPageService pageService)
        {
            this.postService = postService;
            this.pageService = pageService;
        }


        [HttpPost]
        public IActionResult GetString(SearchViewModel search)
        {
            if (ModelState.IsValid)
            {
                return RedirectPermanent($"Tag/{search.Text}");
            }

            return BadRequest();
        }

        [HttpGet("{text}/{page:int?}")]
        public IActionResult SearchTags(string text, int page = 1)
        {
            var posts     = postService.GetPostsByTag(text);
            var pagedList = pageService.GetPagedList(posts, page);

            ViewBag.Controller = "Tag";
  
            var pageViewModel = ModelFactory.Create(pagedList);

            return View("../Search/SearchResult", pageViewModel);
        }
    }
}
