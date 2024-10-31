using CleanArchitecture.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.UserDtos
{
    public class CreateUserResponse
    {
        public Employee Employee { get; set; }
        public bool ProfileCreated { get; set; }
        public bool EmailSent { get; set; }
        public string Message { get; set; }
        public string Email => Employee?.Email; // Propriété calculée pour l'email
    }

}
