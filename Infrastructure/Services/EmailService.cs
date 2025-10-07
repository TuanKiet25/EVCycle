using Application.IServices;
using Infrastructure.Authentication;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;
        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;

            // 2. Tạo nội dung email (có thể là HTML hoặc text)
            var builder = new BodyBuilder();
            builder.HtmlBody = body; // Hoặc builder.TextBody = body;
            email.Body = builder.ToMessageBody();

            // 3. Gửi email
            using var smtp = new SmtpClient();
            try
            {
                // Kết nối đến SMTP server
                await smtp.ConnectAsync(_mailSettings.SmtpServer, _mailSettings.Port, SecureSocketOptions.StartTls);

                // Xác thực tài khoản
                await smtp.AuthenticateAsync(_mailSettings.SenderEmail, _mailSettings.Password);

                // Gửi email đi
                await smtp.SendAsync(email);
            }
            finally
            {
                // Ngắt kết nối
                await smtp.DisconnectAsync(true);
            }
        }
    }
}
