using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.InterfacesUse
{
    public  interface INotificationServiceEmail
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
