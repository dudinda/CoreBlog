using Blog.Models;
using Blog.Models.Data;
using Blog.Models.PostViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{

    public class PostController : Controller
    {
        private BlogContext _context { get; }

        public PostController(BlogContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult OpenPost(int id)
        {
            var post = _context.Posts.Where(opt => opt.Id == id);
            return View(post.First());
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreatePost()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreatePost(PostViewModel viewModel) 
        {
            if(ModelState.IsValid)
            {
 
                var newPost             = new Post(viewModel);
                    newPost.Author      = User.Identity.Name;
                    newPost.IsPublished = true;
                   
             

                if (newPost.IsPublished == true)
                {
                    
                    _context.Add(newPost);
                    _context.SaveChanges();
                }

                return RedirectToActionPermanent("OpenPost", new { newPost, newPost.Id });
            }

            return BadRequest(new { ModelState = ModelState });
        }

    }
}
