using System.ComponentModel.DataAnnotations;


namespace Blog.Models.AccountViewModels
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Range(18, 120, ErrorMessage = "You must be 18 or older.")]
        public int Age { get; set; }
    }
}
