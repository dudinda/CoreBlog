using CoreBlog.Web.Factory;
using CoreBlog.Web.ViewModels.Post;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CoreBlog.Web.Controllers
{
    public sealed partial class PostController
    {
       
        [HttpGet("/api/post/create")]
        public IActionResult GetPostForm()
        {
            return Json(new CreatePostViewModel());
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
                var post = postService.GetPostById(viewModel.Id);
                    post.Tags.Clear();

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
