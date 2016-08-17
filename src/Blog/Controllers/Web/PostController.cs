using Blog.Models;
using Blog.Models.Data;
using Blog.Models.PostViewModels;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;


namespace Blog.Controllers
{
    [ResponseCache(CacheProfileName = "Default")]
    [Route("[controller]")]
    public sealed partial class PostController : Controller
    {
        private ITagService tagService { get; }
        private IPostService postService { get; }

        public PostController(IPostService repository,
                              ITagService tagService)
        {
            this.postService = repository;
            this.tagService = tagService;
        }

        [HttpGet]
        [Route("Open/{id}")]
        public IActionResult OpenPost(int id)
        {
            var post = postService.GetPostById(id);

            var postViewModel = ModelFactory.Create(post);

            return View(postViewModel);
        }


        [Authorize(Roles = "Admin, User")]
        [Route("Create")]
        public IActionResult CreatePost()
        {
            return View();
        }
    }
}
