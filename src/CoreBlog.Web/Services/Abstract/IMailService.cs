using CoreBlog.Data.Context;
using CoreBlog.Web.ViewModels.Account;
using System.Threading.Tasks;

namespace CoreBlog.Web.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(ContactViewModel viewModel);
        Task ConfirmEmailAsync(BlogUser user, string callbackUrl);
        Task ResetPasswordAsync(BlogUser user, string callbackUrl);
    }
}