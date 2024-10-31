using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.LeaveRequestDtos
{
    public class LeaveStatisticsDto
    {
        public int TotalLeaveRequests { get; set; } // Nombre total de demandes de congé
        public int ApprovedLeaveRequests { get; set; } // Nombre de demandes approuvées
        public int RejectedLeaveRequests { get; set; } // Nombre de demandes rejetées
        public int PendingLeaveRequests { get; set; } // Nombre de demandes en attente
        public double AverageLeaveDays { get; set; } // Nombre moyen de jours de congé pris par demande
        public LeaveStatisticsDto(int totalLeaveRequests, int approvedLeaveRequests, int rejectedLeaveRequests,
                                int pendingLeaveRequests, double averageLeaveDays)
        {
            TotalLeaveRequests = totalLeaveRequests;
            ApprovedLeaveRequests = approvedLeaveRequests;
            RejectedLeaveRequests = rejectedLeaveRequests;
            PendingLeaveRequests = pendingLeaveRequests;
            AverageLeaveDays = averageLeaveDays;
        }

    }
}