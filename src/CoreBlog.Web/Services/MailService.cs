using CoreBlog.Data.Context;
using CoreBlog.Web.ViewModels.Account;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
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

        public void SendEmail(ContactViewModel viewModel)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress($"{viewModel.Name}", $"{viewModel.Email}"));
            emailMessage.To.Add(new MailboxAddress(config["Site:Username"], config["Site:Email"]));
            emailMessage.Subject = viewModel.Subject;
            emailMessage.Body = new TextPart("plain") { Text = viewModel.Message };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate(config["Site:Email"], config["Site:Password"]);
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }

        public async Task ConfirmEmailAsync(BlogUser user, string callbackUrl)
        {
           
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(config["Site:Username"], config["Site:Email"]));
            emailMessage.To.Add(new MailboxAddress($"{user.UserName}", $"{user.Email}"));
            emailMessage.Subject = "Thanks for registration!";
            emailMessage.Body = new TextPart("Html") { Text = $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>" };
        
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync(config["Site:Email"], config["Site:Password"]);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
