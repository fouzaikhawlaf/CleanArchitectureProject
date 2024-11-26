using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.InvoicesDto
{
    public class InvoiceDto
    {
        
        public List<InvoiceItemDto>? Products { get; set; }
     
        public string DeliveryDetails { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // Ajoutez cette ligne si elle n'existe pas
        public double TotalAmount { get; set; }
        public double TotalAmountWithTax { get; set; }
    }
}
