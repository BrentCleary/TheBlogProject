﻿
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
        private readonly MailSettings _mailSettings;

        public EmailService(ApplicationDbContext context, IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        //This is the content of the SendContactEmailAsync() method
        //The functional portion of this method is the same as SendEmailAsync(), but the body is pre-set
        public async Task SendContactEmailAsync(string emailFrom, string name, string subject, string htmlMessage)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(_mailSettings.Mail));
            email.Subject = subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = $"<b>{name}</b> has sent you an email and can be reached at: <b>{emailFrom}</b><br/><br/>{htmlMessage}";

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);

            await smtp.SendAsync(email);

            smtp.Disconnect(true);
        }

        public async Task SendEmailAsync(string emailTo, string subject, string htmlMessage)
        {
            var emailSender = _mailSettings.Mail ?? Environment.GetEnvironmentVariable("Mail");

            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(emailSender);
            email.To.Add(MailboxAddress.Parse(emailTo));
            email.Subject = subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = htmlMessage;

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

            try
            {
                var host = _mailSettings.Host ?? Environment.GetEnvironmentVariable("Host");
                var port = _mailSettings.Port != 0 ? _mailSettings.Port : int.Parse(Environment.GetEnvironmentVariable("Port"))!;
                var password = _mailSettings.Password ?? Environment.GetEnvironmentVariable("Password");


                smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);

                await smtp.SendAsync(email);
                smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                throw;
            }

        }
    }
}
