using CleanArchitecture.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.OrderDtos
{
    public class UpdateOrderItemDto
    {
        public int ProductId { get; set; } // Product ID
        public int Quantity { get; set; } // Quantity of the product
        public double Price { get; set; } // Price of the product
        public TVAType TVARate { get; set; }
        public double TVA { get; set; } // TVA applied to the product
        public bool IsArchived { get; set; } // Option pour archiver l'item si nécessaire
    }
}
