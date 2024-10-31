using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.LeaveRequestDtos
{
    public class LeaveRequestHistoryDto
    {
        public string LeaveRequestId { get; set; } // ID de la demande de congé
        public string EmployeeName { get; set; } // Nom de l'employé
        public List<LeaveRequestHistoryEntryDto> HistoryEntries { get; set; } // Liste des entrées d'historique
        public LeaveRequestHistoryDto(string leaveRequestId, string employeeName)
        {
            LeaveRequestId = leaveRequestId;
            EmployeeName = employeeName;
            HistoryEntries = new List<LeaveRequestHistoryEntryDto>();
        }
    }
}