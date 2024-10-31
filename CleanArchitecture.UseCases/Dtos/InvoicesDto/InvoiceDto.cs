using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.InvoicesDto
{
    public class InvoiceDto
    {
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public List<InvoiceItemDto> Products { get; set; }
        public double TaxRate { get; set; }
        public string DeliveryDetails { get; set; }
        public string Status { get; set; } // Ajoutez cette ligne si elle n'existe pas
        public double TotalAmount { get; set; }
        public double TotalAmountWithTax { get; set; }
    }
}
