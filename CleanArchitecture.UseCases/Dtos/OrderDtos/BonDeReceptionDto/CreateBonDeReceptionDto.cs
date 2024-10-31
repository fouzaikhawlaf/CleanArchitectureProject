using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.OrderDtos.BonDeReceptionDto
{
    public class CreateBonDeReceptionDto
    {
        public int OrderSupplierId { get; set; }
        public DateTime ReceivedDate { get; set; }
        public List<CreateBonDeReceptionItemDto> Items { get; set; } = new List<CreateBonDeReceptionItemDto>();
        public bool IsConfirmed { get; set; } = false; // Par défaut, non confirmé
        public bool IsArchived { get; set; } = false; // Par défaut, non archivé
        public bool IsInspected { get; set; } = false; // Par défaut, non inspecté
        public bool IsAccepted { get; set; } = false; // Par défaut, non accepté
        public double DiscrepancyAmount { get; set; } = 0; // Par défaut, pas de différence
    }

}
