using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.OrderDtos.BonDeReceptionDto
{
    public class BonDeReceptionDto
    {
        public int Id { get; set; }
        public int OrderSupplierId { get; set; }
        public DateTime ReceivedDate { get; set; }
        public List<BonDeReceptionItemDto> Items { get; set; } = new List<BonDeReceptionItemDto>();
        public bool IsConfirmed { get; set; }
        public bool IsArchived { get; set; }
        public bool IsInspected { get; set; }
        public bool IsAccepted { get; set; }
        public double DiscrepancyAmount { get; set; }
        public bool IsFlaggedForReview { get; set; }
        public DateTime? InspectionDate { get; set; }

        public double TotalReceivedValue { get; set; } // Optionnel : pour afficher le total
    }

}
