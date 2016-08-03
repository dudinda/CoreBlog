using Blog.Models;
using Blog.Models.Data;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [Route("[controller]/")]
    public class SearchController : Controller
    {
        private IPostService postService;

        public SearchController(IPostService repository)
        {
            this.postService = repository;
        }

        //using prefix Item2 for correct model binding from tuple
        [HttpPost]
        public IActionResult GetString([Bind(Prefix = "Item2")]SearchViewModel search)
        {
            if (ModelState.IsValid)
            {
                return RedirectPermanent($"Search/{search.Text}");
            }

            return new BadRequestResult();
        }

        [HttpGet]
        public IActionResult SearchResult()
        {
            return View();
        }

  
        [HttpGet("{text}/{page:int?}")]
        public IActionResult Pagination(string text = null, int page = 1)
        {

            if(string.IsNullOrEmpty(text))
            {
                ViewBag.text = null;
                return RedirectToAction("SearchResult");
            }

            ViewBag.text = text;

            var posts  = postService.FindPostsByText(text);

            var result = postService.GetPagedPosts(posts, page);

            return View("SearchResult", result);

        }

    }
}
