using CleanArchitecture.Entities.Enum;
using CleanArchitecture.UseCases.Dtos.ItemDtos.ProduitDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.OrderDtos.Devis
{
    public class DevisDto
    {

        public int Id { get; set; }
        public int ClientId { get; set; }
        public List<ProductDto> ?Products { get; set; }
        public double TotalAmount { get; set; }
        public TVAType TVARate { get; set; }
        public double TVA { get; set; }
        public double TotalTVA { get; set; }
        public string Status { get; set; } = string.Empty; // Status as string for display
        public bool IsAccepted { get; set; }
    }
}
