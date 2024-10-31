using CleanArchitecture.UseCases.InterfacesUse;
using System;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using System.Net.Sockets;
using MimeKit;
using MailKit.Net.Smtp;
using CleanArchitecture.Entities.Users;
using MailKit.Security;


namespace CleanArchitecture.UseCases.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly EmailSettings _emailSettings;
        // Constructeur pour passer les paramètres directement
        public EmailService(IConfiguration configuration,EmailSettings emailSettings)
        {
            _configuration = configuration;
            _emailSettings = emailSettings;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Your App Name", _configuration["Smtp:Username"]));
            emailMessage.To.Add(MailboxAddress.Parse(toEmail));
            emailMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = body };
            emailMessage.Body = bodyBuilder.ToMessageBody();

            using var client = new MailKit.Net.Smtp.SmtpClient();
            await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);

            await client.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);

            // Log or display the email details
            Console.WriteLine($"Email sent to: {emailMessage.Subject}");
            Console.WriteLine($"Subject: {toEmail}");
            Console.WriteLine($"Body: {body}");
        }


        public async Task SendEmail(string email, string subject, string messageBody)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("ErpApplication", "no-replayMessage@gmail.com"));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = subject;
                message.Body = new TextPart("plain") { Text = messageBody };

                using var client = new MailKit.Net.Smtp.SmtpClient();
                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("fouzaikhawla123@gmail.com", "lzjr plya dvxa zncs");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"Error sending email: {ex.Message}");
                throw; // Or handle it as needed
            }
        }



    }
}
