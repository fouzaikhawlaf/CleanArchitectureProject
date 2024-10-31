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
        public string UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        // Foreign key pour User

        [JsonIgnore]
        public Employee? User { get; set; }
    }

}
