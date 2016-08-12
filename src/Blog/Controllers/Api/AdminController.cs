using Blog.Models;
using Blog.Models.Account;
using Blog.Models.Data;
using Blog.ViewModels;
using Blog.ViewModels.ControlPanelViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{

    sealed public class AdminController : Controller
    {
        private UserManager<BlogUser> userManager { get; }
        private IPostService postService { get; }

        public AdminController(IPostService postService, UserManager<BlogUser> userManager)
        {
            this.userManager = userManager;
            this.postService = postService;
        }

        public IActionResult Manage()
        {           
            return View();
        }

        [HttpPost("api/admin")]
        public IActionResult ApprovePost([FromBody]PostControlPanelViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //get an existing post then set status
                var post             = postService.GetPostById(viewModel.Id);                    
                    post.IsPublished = viewModel.IsPublished;
  
                if (post.IsPublished)
                {
                    //if it was approved
                    post.Modified = DateTime.UtcNow;
                    postService.UpdatePost(post);
                }
                else
                {
                    //else remove it from the database
                    postService.RemovePost(post);
                }

                if (postService.SaveAll())
                {
                    return View("Manage");
                }
            }
            return BadRequest();
        }

        [HttpGet("api/admin")]
        public JsonResult GetUnpublished()
        {
            //get all unpublished posts
            var posts = postService.GetAllUnpublished();

            var controlViewModel = ModelFactory.Create<PostControlPanelViewModel>(posts);

            return Json(controlViewModel);
        }

    }
}
