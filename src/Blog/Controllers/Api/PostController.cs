using Blog.Models.Data;
using Blog.Models.PostViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Blog.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public sealed partial class PostController
    {
       
        [HttpGet("/api/post/create")]
        public IActionResult GetPostForm()
        {
            var createPostViewModel = new CreatePostViewModel();

            return Json(createPostViewModel);
        }


        [HttpPost("/api/post/submit")]
        public IActionResult GetNewPost([FromBody]CreatePostViewModel viewModel)
        {
            if (ModelState.IsValid)
            {


                var newPost             = ModelFactory.Create(viewModel);
                    newPost.Author      = User.Identity.Name;
                    newPost.PostedOn    = DateTime.UtcNow;

                postService.AddPost(newPost);

                if(postService.SaveAll())
                {
                    ViewBag.Message = "Thanks for joining us!";
                    return Ok();
                }
            }

            return BadRequest();
        }

        [HttpPut("/api/post/update")]
        public IActionResult UpdatePost([FromBody]CreatePostViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var post                 = postService.GetPostById(viewModel.Id);
                var updatedPost          = ModelFactory.Create(post, viewModel);
                    updatedPost.Modified = DateTime.UtcNow;

                postService.UpdatePost(updatedPost);

                if (postService.SaveAll())
                {
                    return Ok();
                }
            }

            return BadRequest();
        }

        [HttpGet("/api/post/get/{id:int}")]
        public IActionResult GetPost(int id)
        {
            try
            {
                var post = postService.GetPostById(id);

                var controlViewModel = ModelFactory.Create<CreatePostViewModel>(post);

                return Json(controlViewModel);
            }
            catch (Exception)
            {
                //no such id
                return BadRequest();
            }
        }

    }
}
