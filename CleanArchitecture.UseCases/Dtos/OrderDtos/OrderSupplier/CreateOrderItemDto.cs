using CleanArchitecture.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.OrderDtos.OrderSupplier
{
    public class CreateOrderItemDto
    {
        public int ProductId { get; set; } // Identifiant du produit
        public int Quantity { get; set; } // Quantité à commander
        public double Price { get; set; } // Prix unitaire
        public TVAType TVARate { get; set; }
        public double TVA { get; set; }
    }
}
