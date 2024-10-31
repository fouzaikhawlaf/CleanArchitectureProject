using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Repository
{
    public class EmailNotificationService : INotificationService
    {
        private readonly SmtpClient _smtpClient;

        public EmailNotificationService(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }

        public async Task SendNotificationAsync(string to, string subject, string message)
        {
            var mailMessage = new MailMessage("no-reply@yourapp.com", to, subject, message);
            await _smtpClient.SendMailAsync(mailMessage);
        }
    }
}
