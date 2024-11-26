using CleanArchitecture.Entities.Enum;
using CleanArchitecture.Entities.Projects;
using CleanArchitecture.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Entities.Leaves
{

    public class LeaveRequest
    {
        [Key] // Assurez-vous d'avoir cet attribut pour définir la clé primaire
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? LeaveRequestId { get; set; }
        [ForeignKey("EmployeeId")]
        public string EmployeeId { get; set; } = string.Empty;
        public string? Reason { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeaveStatus Status { get; set; } // Pending, Approved, Rejected
        public DateTime DateRequested { get; set; }
        public DateTime? DateActioned { get; set; }
        public string ApprovedBy { get; set; } = string.Empty; // Manager or RH


        public Employee? Employee { get; set; }  // Relation avec l'entité Employee (si vous avez une telle entité)

        public DateTime CreatedDate { get; set; }  // Ajout de la date de création
        public DateTime? UpdatedDate { get; set; }  // Ajout de la date de mise à jour (peut être nullable)
        public string? ManagerComment { get; set; }  // Commentaires du manager

        [ForeignKey("ProjectId")]
        public int ProjectId { get; set; }

        public Project? Project { get; set; }
    }
}