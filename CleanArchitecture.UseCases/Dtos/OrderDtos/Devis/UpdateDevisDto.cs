// Application/DTOs/OrderDtos/Devis/UpdateDevisDto.cs
using CleanArchitecture.Entities.Enum;
using CleanArchitecture.UseCases.Dtos.ItemDtos.ProduitDtos;
using System;
using System.Collections.Generic;

namespace CleanArchitecture.UseCases.Dtos.OrderDtos.Devis
{
    public class UpdateDevisDto
    {
        public int Id { get; set; }  // The ID of the Devis to be updated
        public List<ProductDto>? Products { get; set; }  // Updated list of products in the quote
        public double TotalAmount { get; set; }  // Updated total amount (automatically calculated from products)
        public TVAType TVARate { get; set; }
        public double TVA { get; set; }  // Updated total tax amount
        public double TotalTVA { get; set; }  // Updated total amount including tax
        public bool IsAccepted { get; set; }  // Whether the quote is accepted by the client
        public string? Status { get; set; }  // Updated status of the Devis (Pending, Accepted, Rejected, etc.)

        // Optional fields for partial updates can be nullable, or you can apply business rules in the service layer
    }
}
