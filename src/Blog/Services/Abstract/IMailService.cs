using Blog.Models.Account;
using Blog.Models.AccountViewModels;
using System.Threading.Tasks;

namespace Blog.Service
{
    public interface IMailService
    {
        void SendEmail(ContactViewModel viewModel);
        Task ConfirmEmailAsync(BlogUser user, string callbackUrl);
    }
}