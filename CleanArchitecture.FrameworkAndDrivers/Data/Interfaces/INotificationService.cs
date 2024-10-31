using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Interfaces
{
    public interface INotificationService
    {
        Task SendNotificationAsync(string to, string subject, string message);
    }
}
