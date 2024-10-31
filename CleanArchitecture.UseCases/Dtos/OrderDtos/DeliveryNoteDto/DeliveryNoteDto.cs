using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.OrderDtos.DeliveryNoteDto
{
    public class DeliveryNoteDto
    {
        public int Id { get; set; }
        public int OrderClientId { get; set; } // ID of the associated OrderClient
        public DateTime DeliveryDate { get; set; } // Date of delivery
        public bool IsDelivered { get; set; } // Delivery status
        public bool IsArchived { get; set; } // New field for archiving status
        public string Status { get; set; } // Ajoutez cette ligne si elle n'existe pas
        public List<DeliveryItemDto> Items { get; set; }
    }
}
