using CoreBlog.Data.Context;
using CoreBlog.Web.ViewModels.Account;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CoreBlog.Web.Services
{
    public class MailService : IMailService
    {
        private IConfigurationRoot config { get; }

        public MailService(IConfigurationRoot config)
        {
            this.config = config;
        }

        public async Task SendEmailAsync(ContactViewModel viewModel)
        {
            var emailMessage         = new SendGrid.SendGridMessage();
                emailMessage.From    =  new MailAddress($"{viewModel.Email}", $"{viewModel.Name}");          
                emailMessage.Subject = viewModel.Subject;
                emailMessage.Text    = viewModel.Message;
                emailMessage.AddTo(config["Site:Email"]);

            var transportWeb = new SendGrid.Web(config["SendGrid:ApiKey"]);
            await transportWeb.DeliverAsync(emailMessage);
        }

        public async Task ConfirmEmailAsync(BlogUser user, string callbackUrl)
        {

            var emailMessage         = new SendGrid.SendGridMessage();
                emailMessage.From    = new MailAddress(config["Site:Email"], config["Site:Username"]);       
                emailMessage.Subject = "Thanks for registration!";
                emailMessage.Text    = $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>";
                emailMessage.AddTo($"{user.Email}");

            var transportWeb = new SendGrid.Web(config["SendGrid:ApiKey"]);
            await transportWeb.DeliverAsync(emailMessage);
        }

        public async Task ResetPasswordAsync(BlogUser user, string callbackUrl)
        {
            var emailMessage         = new SendGrid.SendGridMessage();
                emailMessage.From    = new MailAddress(config["Site:Email"], config["Site:Username"]);
                emailMessage.Subject = "Password reset.";
                emailMessage.Text    = $"Please use the following link to <a href='{callbackUrl}'>reset your password</a>.";
                emailMessage.AddTo($"{user.Email}");

            var transportWeb = new SendGrid.Web(config["SendGrid:ApiKey"]);
            await transportWeb.DeliverAsync(emailMessage);
        }
    }
}
