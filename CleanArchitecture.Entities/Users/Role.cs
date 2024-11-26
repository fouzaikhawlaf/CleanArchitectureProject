using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Entities.Users
{
    public class Role
    {
        [Key] // Assurez-vous d'avoir cet attribut pour définir la clé primaire
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string RoleId { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty; // Admin, Manager, RH, Employer, Commerciale

        public ICollection<UserRole> ? UserRoles { get; set; }
    }
}
