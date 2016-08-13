using Blog.Models;
using Blog.Models.Data;
using Blog.Models.PostViewModels;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;


namespace Blog.Controllers
{

    sealed public class PostController : Controller
    {
        private ITagService tagService { get; }

        private IPostService postService { get; }

        public PostController(IPostService repository,
                              ITagService tagService)
        {
            this.postService = repository;
            this.tagService = tagService;
        }

        [HttpGet("Open/{id}")]
        public IActionResult OpenPost(int id)
        {
            var post = postService.GetPostById(id);

            var postViewModel = ModelFactory.Create(post);

            return View(postViewModel);
        }

        [Authorize(Roles ="Admin, User")]
        [HttpGet]
        public IActionResult CreatePost()
        {
            var postCreateViewModel  = new PostCreateViewModel();
           
            return View(postCreateViewModel);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public IActionResult CreatePost(PostCreateViewModel viewModel) 
        {
            if (ModelState.IsValid)
            {
                
 
                var newPost             = ModelFactory.Create(viewModel);
                    newPost.Author      = User.Identity.Name;
                    newPost.PostedOn    = DateTime.UtcNow;
                    newPost.IsPublished = false;

             
                tagService.AddTags(newPost.Tags);
                postService.AddPost(newPost);
                postService.SaveAll();
              

                return RedirectToActionPermanent("OpenPost", new { newPost, newPost.Id });
            }

            return NotFound();
        }

    }
}
