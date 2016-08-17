using Blog.Models;
using Blog.Models.Data;
using Blog.Service;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [Route("[controller]/")]
    [ResponseCache(CacheProfileName = "Default")]
    sealed public class SearchController : Controller
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
            if (ModelState.IsValid)
            {
                return RedirectToAction("SearchResult", new { text = search.Text });
            }
           
            return BadRequest();
        }

        [HttpGet]
        public IActionResult SearchResult()
        {
            return View();
        }

  
        [HttpGet("{text}/{page:int?}")]
        public IActionResult SearchResult(string text = null, int page = 1)
        {
           
            var posts     = postService.GetPostsByText(text);
            var pagedList = pageService.GetPagedList(posts, page);

            ViewBag.Controller = "Search";
      
            var pageViewModel = ModelFactory.Create(pagedList);

            

            return View("SearchResult", pageViewModel);

        }

    }
}
