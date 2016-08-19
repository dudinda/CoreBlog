using Blog.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



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
