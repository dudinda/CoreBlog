using CoreBlog.Web.Factory;
using CoreBlog.Web.Services;
using CoreBlog.Web.ViewModels.Page;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CoreBlog.Web.Controllers
{
    [ResponseCache(CacheProfileName = "Default")]
    [Route("[controller]")]
    public sealed class SearchController : Controller
    {
        private IPageService pageService { get; }
        private IPostService postService { get; }

        public SearchController(IPostService postService, IPageService pageService)
        {
            this.postService = postService;
            this.pageService = pageService;
        }


        [HttpPost]
        public IActionResult GetString(SearchViewModel search)
        {
            //due to [DisplayFormat] attribute issue
            if (search.Text == null)
            {
                return RedirectToAction("SearchResult", new { text = "" });
            }
            
            if (ModelState.IsValid)
            {
                return RedirectToAction("SearchResult", new { text = search.Text });
            }

            return BadRequest();

        }


        [HttpGet("{text}/{page:int?}")]
        public IActionResult SearchResult(string text, int page = 1)
        {
            ViewBag.Controller = "Search";


            var posts     = postService.GetPostsByText(text);
            var pagedList = pageService.GetPagedList(posts, page);


            var pageViewModel = ModelFactory.Create(pagedList);



            return View(pageViewModel);

        }

    }
}
