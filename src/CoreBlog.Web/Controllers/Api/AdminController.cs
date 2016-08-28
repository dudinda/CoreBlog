using CoreBlog.Data.Utility;
using CoreBlog.Web.Factory;
using CoreBlog.Web.ViewModels.ControlPanel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBlog.Web.Controllers
{

    public sealed partial class AdminController
    {

        [HttpGet("/api/admin/users")]
        public IActionResult GetUsers()
        {
            try
            {
                //get all users
                var users = userManager.Users.ToList();
                var controlViewModel = ModelFactory.Create(users);

                return Json(controlViewModel);
            }
            catch(Exception)
            {
                logger.LogError("Failed to get users.");
                return BadRequest();
            }
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

                            logger.LogInformation($"{user.UserName} is active.");

                            return Json(user.isBanned);

                        }
                    }
                }
            }
            logger.LogError($"Failed to unban {viewModel.UserName}");
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

                            logger.LogInformation($"{user.UserName} is banned.");

                            return Json(user.isBanned);

                        }
                    }            
                }
            }
            logger.LogError($"Failed to ban {viewModel.UserName}");
            return BadRequest();
        }


        [HttpPost("/api/admin/delete")]
        public IActionResult DeletePost([FromBody]PostControlPanelViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var post = postService.GetPostById(viewModel.Id);

                if(post.Image.Base64 != null)
                {
                    post.Image.Delete();
                }

                postService.RemovePost(post);

                if(postService.SaveAll())
                {
                    logger.LogInformation($"{post.Title} no longer exists.");
                    return Ok();
                }
                
            }
            logger.LogError($"Failed to remove: {viewModel.Title}");
            return BadRequest();
        }

        [HttpPost("/api/admin/approve")]
        public IActionResult ApprovePost([FromBody]PostControlPanelViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //get an existing post then set isPublished status
                var post             = postService.GetPostById(viewModel.Id);
                    post.IsPublished = true;

                if (post.Image.Base64 != null)
                {
                    post.Image.ToImage();
                }

                postService.UpdatePost(post);
        
                if (postService.SaveAll())
                {
                    logger.LogInformation($"{post.Title} is published.");
                    return Ok();
                }
            }
            logger.LogError($"Failed to publish: {viewModel.Title}");
            return BadRequest();
        }

        [HttpGet("/api/admin/published")]
        public IActionResult GetAll()
        {
            try
            {
                //get all published posts
                var posts = postService.GetAllPublished();

                var controlViewModel = ModelFactory.Create<PostControlPanelViewModel>(posts);

                return Json(controlViewModel);
            }
            catch(Exception)
            {
                logger.LogError("Failed to get published posts.");
                return BadRequest();
            }
        }

        [HttpGet("/api/admin/unpublished")]
        public IActionResult GetUnpublished()
        {
            try
            {
                //get all unpublished posts
                var posts = postService.GetAllUnpublished();

                var controlViewModel = ModelFactory.Create<PostControlPanelViewModel>(posts);

                return Json(controlViewModel);
            }
            catch(Exception)
            {
                logger.LogError("Failed to get unpublished posts.");
                return BadRequest();
            }
        }

    }
}
