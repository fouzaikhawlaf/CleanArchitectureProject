using CleanArchitecture.UseCases.Dtos.ItemDtos.ProduitDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.InvoicesDto
{

    public class ExternalDataDto
    {
        public decimal TaxRate { get; set; }
        public string? DeliveryDetails { get; set; }
        public List<ProductDto> ?Products { get; set; }
        public string? ClientName { get; set; }
        public string? ClientAddress { get; set; }
    }
}
