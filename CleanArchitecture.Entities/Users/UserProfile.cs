using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CleanArchitecture.Entities.Users
{
    public class UserProfile
    {
        [Key] // Assurez-vous d'avoir cet attribut pour définir la clé primaire
        [ForeignKey("Employee")]
        public string UserId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        // Foreign key pour User

        [JsonIgnore]
        public Employee? User { get; set; }
    }

}
