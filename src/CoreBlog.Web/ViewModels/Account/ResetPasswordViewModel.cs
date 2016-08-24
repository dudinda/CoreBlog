using System.ComponentModel.DataAnnotations;

namespace CoreBlog.Web.ViewModels.Account
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }

      
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; }
    }
}
