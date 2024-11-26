using CleanArchitecture.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.LeaveRequestDtos
{
    public class UpdateLeaveRequestDto
    {
        // Identifiant de la demande de congé (nécessaire pour spécifier quelle demande est mise à jour)
        public string LeaveRequestId { get; set; } = string.Empty;

        // Date de début du congé
        public DateTime StartDate { get; set; }

        // Date de fin du congé
        public DateTime EndDate { get; set; }

        // Motif du congé
        public string Reason { get; set; } = string.Empty;

        // Statut de la demande (peut être modifié par un RH ou un Manager)
        public LeaveStatus Status { get; set; }

        // Commentaire ou réponse du RH/Manager
        public string ManagerComment { get; set; } = string.Empty;
    }

}
