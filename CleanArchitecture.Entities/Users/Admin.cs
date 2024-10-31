using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Entities.Users
{
    public class Admin
    {
        [Key] // Assurez-vous d'avoir cet attribut pour définir la clé primaire
        public int AdminId { get; set; }
        public string? Nom { get; set; }
        public string? Email { get; set; }
        // Navigation property for managed employees
        public List<Employee> ManagedEmployees { get; set; } = new List<Employee>();
        // Autres propriétés spécifiques à l'Admin
    }
}
