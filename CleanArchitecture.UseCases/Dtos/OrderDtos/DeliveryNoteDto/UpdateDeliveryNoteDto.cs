using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.OrderDtos.DeliveryNoteDto
{
    public class UpdateDeliveryNoteDto
    {
        public int Id { get; set; }
        public DateTime DeliveryDate { get; set; } // Updated delivery date
        public bool IsDelivered { get; set; } // Updated delivery status
        public bool IsArchived { get; set; }
        public List<DeliveryItemDto> Items { get; set; }
    }
}
