using CleanArchitecture.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.OrderDtos
{
    public class UpdateOrderClientDto
    {
        public double SaleAmount { get; set; } // Pre-discount amount
        public double TotalAmount { get; set; } // Total amount after discount and TVA
        public TVAType TVARate { get; set; }
        public double TVA { get; set; }
        public double TotalTVA { get; set; } // Total TVA (tax)
        public double Discount { get; set; } // Discount applied to the order
        public DateTime OrderDate { get; set; } // Date of the order
        public List<UpdateOrderItemDto> OrderItems { get; set; } = new List<UpdateOrderItemDto>(); // List of updated order items

     
    }
}
