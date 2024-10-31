using CleanArchitecture.UseCases.Dtos.OrderDtos.OrderSupplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.OrderDtos
{
    public class CreateOrderClientDto
    {
        public int ClientId { get; set; } // Client ID
        public double SaleAmount { get; set; } // Pre-discount amount
        public double TotalAmount { get; set; } // Total amount after discount and TVA
        public double TotalTVA { get; set; } // Total TVA (tax)
        public double Discount { get; set; } // Discount applied to the order
        public DateTime OrderDate { get; set; } // Date of the order
        public List<CreateOrderItemDto> OrderItems { get; set; } = new List<CreateOrderItemDto>(); // List of order items
    }
}

