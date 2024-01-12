
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit;
//using System.Net.Mail;
using TheBlogProject.Data;
using TheBlogProject.ViewModels;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace TheBlogProject.Services
{
    public class EmailService : IBlogEmailSender
    {
        private readonly ApplicationDbContext _context;
        private readonly MailSettings _mailSettings;

        public EmailService(ApplicationDbContext context, IOptions<MailSettings> mailSettings)
        {
            _context = context;
            _mailSettings = mailSettings.Value;
            
        }

        public Task SendContactEmailAsync(string emailFrom, string name, string subject, string htmlMessage)
        {
            throw new NotImplementedException();
        }


        public async Task SendEmailAsync(string emailTo, string subject, string htmlMessage)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(emailTo));
            email.Subject = subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = htmlMessage;

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);

            await smtp.SendAsync(email);

            smtp.Disconnect(true);

        }
    }
}
