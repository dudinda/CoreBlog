using Blog.Models.AccountViewModels;
using MailKit.Net.Smtp;
using MimeKit;

namespace Blog.Service
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
    }
}
