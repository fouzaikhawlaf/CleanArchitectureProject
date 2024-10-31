using CleanArchitecture.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.LeaveRequestDtos
{
    public class LeaveRequestDto
    {
        public string LeaveRequestId { get; set; }
        public string EmployeeName { get; set; }
        public string Reason { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeaveStatus Status { get; set; }
        public string ApprovedBy { get; set; }
        public string EmployeeId { get; set; }  // Identifiant de l'employé
    

        public DateTime CreatedDate { get; set; }  // Date de création
        public DateTime? UpdatedDate { get; set; }  // Date de mise à jour (peut être nullable)
        public string ManagerComment { get; set; }  // Commentaires du manager
    }
}
