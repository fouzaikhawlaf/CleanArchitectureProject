using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.InvoicesDto
{
    public class InvoiceSupplierDto : InvoiceDto
    {
        public int InvoiceId { get; set; }
        public string? SupplierName { get; set; }
        public string? SupplierAddress { get; set; }
        public DateTime InvoiceDate { get; set; }
        public List<InvoiceItemDto> ?LineItems { get; set; }
        public double? TaxRate { get; set; }
        public double Total { get; set; }
        public string? DeliveryDetails { get; set; }
    }
}
