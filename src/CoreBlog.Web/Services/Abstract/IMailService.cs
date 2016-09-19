using CoreBlog.Data.Context;
using CoreBlog.Web.ViewModels.Account;
using System.Threading.Tasks;

namespace CoreBlog.Web.Services
{
    public interface IMailService
    {
        void SendEmail(ContactViewModel viewModel);
        Task ConfirmEmailAsync(BlogUser user, string callbackUrl);
        Task ResetPasswordAsync(BlogUser user, string callbackUrl);
    }
}