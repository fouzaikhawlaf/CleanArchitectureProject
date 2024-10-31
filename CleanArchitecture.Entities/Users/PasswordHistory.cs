using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Entities.Users
{
    public class PasswordHistory
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public DateTime ChangedAt { get; set; }
        public DateTime DateAdded { get; set; }
        public string UserId { get; set; }
        public Employee User { get; set; }
    }
}
