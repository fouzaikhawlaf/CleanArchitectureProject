// Application/DTOs/OrderDtos/Devis/CreateDevisDto.cs
using CleanArchitecture.Entities.Enum;
using CleanArchitecture.UseCases.Dtos.ItemDtos.ProduitDtos;
using System;
using System.Collections.Generic;

namespace CleanArchitecture.UseCases.Dtos.OrderDtos.Devis
{
    public class CreateDevisDto
    {
        public int Id { get; set; }  // The client making the Devis (Quote)
        public int ClientId { get; set; }  // The client making the Devis (Quote)
        public List<ProductDto>?Products { get; set; }  // List of products in the quote
        public double TotalAmount { get; set; }  // Total amount before taxes (automatically calculated from products)
        public TVAType TVARate { get; set; }
        public double TVA { get; set; }  // Total tax amount (can be calculated automatically)
        public double TotalTVA { get; set; }  // Total amount including tax (can be calculated automatically)
        public bool IsAccepted { get; set; }  // Whether the quote is accepted by the client
        public string Status { get; set; } = string.Empty; // Status of the Devis (Pending, Accepted, Rejected, etc.)

        // You can add validation attributes here if necessary, e.g., [Required], [Range], etc.
    }
}
