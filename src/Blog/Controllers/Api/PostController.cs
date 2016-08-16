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
    public sealed partial class PostController
    {
        [Authorize(Roles = "Admin, User")]
        [HttpGet("api/post/create")]
        public IActionResult GetPostForm()
        {
            var postCreateViewModel = new PostCreateViewModel();

            return Json(postCreateViewModel);
        }


        [Authorize(Roles = "Admin, User")]
        [HttpPost("api/post/submit")]
        public IActionResult GetnewPost([FromBody]PostCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {


                var newPost = ModelFactory.Create(viewModel);
                newPost.Author = User.Identity.Name;
                newPost.PostedOn = DateTime.UtcNow;
                newPost.IsPublished = false;


                tagService.AddTags(newPost.Tags);
                postService.AddPost(newPost);
                postService.SaveAll();


                return RedirectToActionPermanent("OpenPost", new { newPost, newPost.Id });
            }

            return BadRequest();
        }

    }
}
