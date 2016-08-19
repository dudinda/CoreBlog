using Blog.Models.Data;
using Blog.Models.PostViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Blog.Controllers
{
    public sealed partial class PostController
    {
        [Authorize(Roles = "Admin, User")]
        [HttpGet("/api/post/create")]
        public IActionResult GetPostForm()
        {
            var createPostViewModel = new CreatePostViewModel();

            return Json(createPostViewModel);
        }


        [Authorize(Roles = "Admin, User")]
        [HttpPost("/api/post/submit")]
        public IActionResult GetNewPost([FromBody]CreatePostViewModel viewModel)
        {
            if (ModelState.IsValid)
            {


                var newPost = ModelFactory.Create(viewModel);
                newPost.Author = User.Identity.Name;
                newPost.PostedOn = DateTime.UtcNow;
                newPost.IsPublished = false;

                postService.AddPost(newPost);

                if(postService.SaveAll())
                {
                    ViewBag.Message = "Thanks for joining us!";
                    return Ok();
                }
            }

            return BadRequest();
        }

    }
}
