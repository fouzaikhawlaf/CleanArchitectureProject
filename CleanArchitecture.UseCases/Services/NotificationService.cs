using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.UseCases.InterfacesUse;

namespace CleanArchitecture.UseCases.Services
{
    public class NotificationService : INotificationServiceEmail
    {
        private readonly string _smtpHost = "smtp.gmail.com"; // Remplacer par l'hôte SMTP de votre fournisseur (ex : smtp.gmail.com pour Gmail)
        private readonly int _smtpPort = 587; // Le port SMTP, souvent 587 pour TLS ou 465 pour SSL
        private readonly string _smtpUser = "fouzaikhawla123@gmail.com"; // Votre e-mail (par exemple, pour l'authentification)
        private readonly string _smtpPass = "azerty123@A"; // Votre mot de passe ou un token d'application

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                using (var smtpClient = new SmtpClient(_smtpHost, _smtpPort))
                {
                    smtpClient.EnableSsl = true; // Activer SSL (ou TLS) si nécessaire
                    smtpClient.Credentials = new NetworkCredential(_smtpUser, _smtpPass);

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_smtpUser),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = false // Si vous souhaitez envoyer un e-mail HTML, mettez `true`
                    };

                    mailMessage.To.Add(to);

                    // Envoyer l'e-mail de façon asynchrone
                    await smtpClient.SendMailAsync(mailMessage);
                }

                Console.WriteLine("E-mail envoyé avec succès.");
            }
            catch (Exception ex)
            {
                // Logique en cas d'erreur
                Console.WriteLine($"Erreur lors de l'envoi de l'e-mail : {ex.Message}");
                throw; // Optionnel, rejeter l'exception si vous souhaitez la propager
            }
        }
    }
}
