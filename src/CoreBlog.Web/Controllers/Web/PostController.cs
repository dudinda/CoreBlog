using CoreBlog.Web.Factory;
using CoreBlog.Web.Services;
using CoreBlog.Web.ViewModels.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CoreBlog.Web.Controllers
{
    [ResponseCache(CacheProfileName = "Default")]
    [Route("[controller]")]
    [Authorize(Policy = "Active")]
    public sealed partial class PostController : Controller
    {
        private IPostService postService { get; }

        public PostController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpGet("Open/{id:int}")]
        [AllowAnonymous]
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


        [HttpGet("Create")]
        public IActionResult CreatePost()
        {
            return View();
        }
    }
}
