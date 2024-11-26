using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.LeaveRequestDtos
{
    public class LeaveRequestHistoryEntryDto
    {
        public DateTime ChangeDate { get; set; } // Date du changement
        public string ChangeDescription { get; set; } = string.Empty; // Description du changement
        public string ChangedBy { get; set; } = string.Empty;// Utilisateur qui a effectué le changement
        public LeaveRequestHistoryEntryDto(DateTime changeDate, string changeDescription, string changedBy)
        {
            ChangeDate = changeDate;
            ChangeDescription = changeDescription;
            ChangedBy = changedBy;
        }
    }
}