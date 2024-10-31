using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.UserDtos
{
    public class UpdateUserDto
    {
        public string Id { get; set; } // ID de l'utilisateur qui sera mis à jour
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string Address { get; set; }
        // Ajoute toutes les propriétés qui sont modifiables ici
    }
}
