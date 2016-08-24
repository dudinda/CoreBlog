using CoreBlog.Data.Context;
using CoreBlog.Web.ViewModels.Account;
using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace CoreBlog.Web.Services
{
    public class MailService : IMailService
    {
       
        public void SendEmail(ContactViewModel viewModel)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress($"{viewModel.Name}", $"{viewModel.Email}"));
            emailMessage.To.Add(new MailboxAddress("Dan", "enragesoft@gmail.com"));
            emailMessage.Subject = viewModel.Subject;
            emailMessage.Body = new TextPart("plain") { Text = viewModel.Message };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("user", "pwd");
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }

        public async Task ConfirmEmailAsync(BlogUser user, string callbackUrl)
        {
           
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress($"Dan", $"enragesoft@gmail.com"));
            emailMessage.To.Add(new MailboxAddress($"{user.UserName}", $"{user.Email}"));
            emailMessage.Subject = "Thanks for registration!";
            emailMessage.Body = new TextPart("Html") { Text = $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>" };
        
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("user", "pwd");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
