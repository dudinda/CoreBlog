using Blog.Models.Data;
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
        private IPostService postService { get; }

        public PostController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpGet]
        [Route("Open/{id}")]
        public IActionResult OpenPost(int id)
        {
            try
            {
                var post = postService.GetPostById(id);

                var postViewModel = ModelFactory.Create<PostViewModel>(post);

                return View(postViewModel);
            }
            catch (Exception)
            { 
                //no such id
                return NotFound();
            }
        }


        [Authorize(Roles = "Admin, User")]
        [Route("Create")]
        public IActionResult CreatePost()
        {
            return View();
        }
    }
}
