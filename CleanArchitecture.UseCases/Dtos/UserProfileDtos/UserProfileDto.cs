using CleanArchitecture.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.UserProfileDtos
{
    public class UserProfileDto
    {
        public string? Id { get; set; }
        public string? UserId { get; set; } // ID de l'utilisateur associé
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Department { get; set; }
      
    }

}
