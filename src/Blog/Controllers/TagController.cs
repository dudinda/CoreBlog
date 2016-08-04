using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Models.Data;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Controllers
{
    public class TagController : Controller
    {
        private IPostService service;

        public TagController(IPostService service)
        {
            this.service = service;
        }


        [HttpGet]
        public IActionResult TagsResult()
        {
            return View();
        }

        // GET: /<controller>/
        [HttpGet("[controller]/{text}/{page:int}")]
        public IActionResult SearchTags(string text, int page = 1)
        {
            var result = service.GetPostByTag(text);
            

            return View("TagsResult", result);
        }
    }
}
