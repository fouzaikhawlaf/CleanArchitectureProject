using CleanArchitecture.Entities.Leaves;
using CleanArchitecture.Entities.Notifications;
using CleanArchitecture.Entities.Projects;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Entities.Users
{
    public class Employee : IdentityUser
    {
        // Nom complet de l'utilisateur
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int LeaveBalance { get; set; }
        public bool IsArchived { get; set; } // Si c'est une propriété
        public bool emailSent { get; set; } = false; // Si c'est une propriété
        public bool MustChangePassword { get; set; } = false;
        public string Department { get; set; } = string.Empty;
        // Token pour rafraîchir la session
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiryTime { get; set; }

        // Foreign key for Admin
        public int? AdminId { get; set; }
        public Admin? Admin { get; set; }
        // Foreign key to Role
    

        public List<UserRole> Roles { get; set; } = new List<UserRole>();
        public List<Notification> Notifications { get; set; } = new List<Notification>();

        // Relations avec UserProfile et UserAccount
        public UserProfile UserProfile { get; set; } 
       

        // Projets et tâches assignés
        [InverseProperty("TeamMembers")]
        public ICollection<Project> Projects { get; set; } = new List<Project>();

        public ICollection<TaskProject> AssignedTasks { get; set; } = new List<TaskProject>();

        // Congés
        [InverseProperty("Employee")]
        public ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();

        // Méthode pour assigner une tâche
        public void AssignTask(TaskProject task)
        {
            if (!AssignedTasks.Contains(task))
            {
                AssignedTasks.Add(task);
                task.AssignedTo = this;
            }
        }
    }
}
