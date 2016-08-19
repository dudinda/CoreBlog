using Blog.Models.AccountViewModels;

namespace Blog.Service
{
    public interface IMailService
    {
        void SendEmail(ContactViewModel viewModel);
    }
}