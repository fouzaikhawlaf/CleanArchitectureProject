using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Entities.Users
{
    public class UserRole
    {
        [Key] // Assurez-vous d'avoir cet attribut pour définir la clé primaire
        public string UserId { get; set; } = string.Empty;// Change this to match IdentityUser's Id type
        public Employee? User { get; set; }

        [ForeignKey("RoleId")]
        public string RoleId { get; set; } = string.Empty;
        public Role? Role { get; set; }
    }
}
