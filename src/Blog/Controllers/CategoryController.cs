using Blog.Models.Data;
using Blog.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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


        [HttpGet]
        public IActionResult CategoryResult()
        {
            return View();
        }

        // GET: /<controller>/
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
