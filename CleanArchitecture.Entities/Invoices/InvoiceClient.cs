using CleanArchitecture.Entities.Orders;
using CleanArchitecture.Entities.Orders.DeliveryNotes;
using CleanArchitecture.Entities.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Entities.Invoices
{
    public class InvoiceClient :Invoice
    {
        [ForeignKey("OrderClientId")]
        public int OrderClientId { get; set; }
        [ForeignKey("ClientId")]
        public int ClientId { get; set; }
        public string? ClientName { get; set; }
        public string? ClientAddress { get; set; }
        public OrderClient? OrderClient { get; set; }

        [ForeignKey("DeliveryNoteId")]
        public int DeliveryNoteId { get; set; }
        public string? DeliveryDetails { get; set; }
        public DeliveryNote? DeliveryNote { get; set; }
        public ICollection<Sale> Sales  { get; set; } = new List<Sale>();


    }
}
