using System.ComponentModel.DataAnnotations;

namespace CoreBlog.Web.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
