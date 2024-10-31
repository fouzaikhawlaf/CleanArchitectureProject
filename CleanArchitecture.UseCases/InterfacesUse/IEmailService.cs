using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.InterfacesUse
{
    public interface IEmailService
    {
        
        Task SendEmailAsync(string to, string subject, string body);
        //  Task SendPasswordResetEmail(string email, string defaultPassword);
        Task SendEmail(string email, string subject, string messageBody);
    }
}