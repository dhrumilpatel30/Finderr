using MimeKit;
using MailKit.Net.Smtp;
using MailKit;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

namespace Finderr.Services
{
    public class MailSender : IEmailSender
    {

        public MailSender()
        {
        }


        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            AuthDetails Options = new()
            {
                Email = Environment.GetEnvironmentVariable("EMAIL"),
                Password = Environment.GetEnvironmentVariable("PASSWORD")
            };
            if (string.IsNullOrEmpty(Options.Email))
            {
                throw new Exception("Null Email");
            }
            await Task.Run(() => SendMail(Options, subject, message, toEmail));
        }

        static public void SendMail(AuthDetails authDetails, string subject, string body, string toMail)
        {

            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Sender Name", authDetails.Email));
            email.To.Add(new MailboxAddress("Receiver Name", toMail));

            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body
            };

            using var smtp = new SmtpClient();

            smtp.Connect("smtp.gmail.com", 587, false);

            smtp.Authenticate(authDetails.Email, authDetails.Password);

            string respose = smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
