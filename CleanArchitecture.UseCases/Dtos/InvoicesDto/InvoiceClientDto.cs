using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.InvoicesDto
{
    public class InvoiceClientDto :InvoiceDto
    {
        public int InvoiceId { get; set; }
        public string? ClientName { get; set; }
        public string? ClientAddress { get; set; }
        public DateTime InvoiceDate { get; set; }
        public double? TaxRate { get; set; }
        public string? DeliveryDetails { get; set; }
        public ICollection<InvoiceItemDto>? LineItems { get; set; }
        public double Total { get; set; }
    }
}
