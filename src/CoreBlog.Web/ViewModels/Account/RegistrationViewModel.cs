using System.ComponentModel.DataAnnotations;

namespace CoreBlog.Web.ViewModels.Account
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 10 characters")]
        [Display(Prompt = "Enter your username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [Display(Prompt = "Enter your email address")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [Display(Prompt = "Enter your password")]
        [MinLength(8)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords don't match.")]
        [Display(Prompt = "Confirm password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        [Display(Prompt = "Enter your age")]
        [Range(18, 120, ErrorMessage = "You must be 18 or older.")]
        public int Age { get; set; }
    }
}
