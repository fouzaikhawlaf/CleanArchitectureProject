using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.OrderDtos.BonDeReceptionDto
{
    public class UpdateBonDeReceptionDto
    {
        public DateTime ReceivedDate { get; set; }
        public List<UpdateBonDeReceptionItemDto> Items { get; set; } = new List<UpdateBonDeReceptionItemDto>();
        public bool IsConfirmed { get; set; }
        public bool IsArchived { get; set; }
        public bool IsInspected { get; set; }
        public bool IsAccepted { get; set; }
        public double DiscrepancyAmount { get; set; }
    }

}
