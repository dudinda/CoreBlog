using Microsoft.AspNetCore.Mvc;


namespace CoreBlog.Web.Controllers
{
    public sealed class ErrorsController : Controller
    {
        
        public IActionResult Error(int code)
        {
            ViewData["Code"] = code;
            
            if(code == 404)
            {
                ViewData["Message"] = "Page not found!";
            }
            else if(code == 500)
            {
                ViewData["Message"] = "Internal server error!";
            }
            else
            {
                ViewData["Message"] = "Something went wrong... try again later!";
            }

            return View();
        }
    }
}
