using System.ComponentModel.DataAnnotations;

namespace CoreBlog.Web.ViewModels.Account
{
    public class SendResetPasswordViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [Display(Prompt = "Enter your email address")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
