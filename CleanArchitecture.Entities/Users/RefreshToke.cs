using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Entities.Users
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty; // Ensure non-nullable
        public string UserId { get; set; } = string.Empty; // Ensure non-nullable
        public DateTime ExpiryDate { get; set; }
    }

}
