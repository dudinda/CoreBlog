using Blog.Models.Data;
using Blog.ViewModels.ControlPanelViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{

    public sealed partial class AdminController
    {

        [HttpGet("/api/admin/users")]
        public JsonResult GetUsers()
        {
            //get all users
            var users = userManager.Users.ToList();
            var controlViewModel = ModelFactory.Create(users);

            return Json(controlViewModel);
        }
       
        [HttpPost("/api/admin/unban")]
        public async Task<IActionResult> UnbanAsync([FromBody]UserControlPanelViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(viewModel.UserName);
            
                //if user exists
                if (user != null)
                {
                    if (await userManager.IsInRoleAsync(user, "Banned"))
                    {
                        user.isBanned = false;
                        var updateUserResult = await userManager.UpdateAsync(user);

                        if (updateUserResult.Succeeded)
                        {

                            var addToBannedResult    = await userManager.AddToRoleAsync(user, "User");
                            var removeFromUserResult = await userManager.RemoveFromRoleAsync(user, "Banned");

                            return Json(user.isBanned);

                        }
                    }
                }
            }

            return BadRequest();
        }

        [HttpPost("/api/admin/ban")]
        public async Task<IActionResult> BanAsync([FromBody]UserControlPanelViewModel viewModel)
        {
            
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(viewModel.UserName);
              
                //if user exists
                if (user != null)
                {
                    if (await userManager.IsInRoleAsync(user, "User"))
                    {
                        user.isBanned = true;

                        var updateUserResult = await userManager.UpdateAsync(user);

                        if (updateUserResult.Succeeded)
                        {
                            var addToBannedResult    = await userManager.AddToRoleAsync(user, "Banned");
                            var removeFromUserResult = await userManager.RemoveFromRoleAsync(user, "User");

                            return Json(user.isBanned);

                        }
                    }            
                }
            }

            return BadRequest();
        }

        [HttpPost("/api/admin/approve")]
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
                    return Json(viewModel);
                }
            }
            return BadRequest();
        }

        [HttpGet("/api/admin/published")]
        public JsonResult GetAll()
        {
            //get all unpublished posts
            var posts = postService.GetAll();

            var controlViewModel = ModelFactory.Create<PostControlPanelViewModel>(posts);

            return Json(controlViewModel);
        }

        [HttpGet("/api/admin/unpublished")]
        public JsonResult GetUnpublished()
        {
            //get all unpublished posts
            var posts = postService.GetAllUnpublished();

            var controlViewModel = ModelFactory.Create<PostControlPanelViewModel>(posts);

            return Json(controlViewModel);
        }

    }
}
