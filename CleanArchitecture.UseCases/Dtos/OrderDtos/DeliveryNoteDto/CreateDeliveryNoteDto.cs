using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.OrderDtos.DeliveryNoteDto
{
    public class CreateDeliveryNoteDto
    {
        public int OrderClientId { get; set; } // ID of the associated OrderClient
        public DateTime DeliveryDate { get; set; } // Date of delivery
        public List<DeliveryItemDto> Items { get; set; }
    }
}
