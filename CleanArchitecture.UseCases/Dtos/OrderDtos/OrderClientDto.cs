using CleanArchitecture.UseCases.Dtos.ClientDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.OrderDtos
{
    public class OrderClientDto : OrderDto 
    {
        public int ClientId { get; set; }
        public ClientDto? Client { get; set; } 
        public string? ClientName { get; set; } 
        public DateTime SaleDate { get; set; }
        public double SaleAmount { get; set; }
        public double TotalAmount { get; set; }
        public double TotalTVA { get; set; }
        public double Discount { get; set; }
        public bool IsDelivered { get; set; } = false; // Order delivery status
        public List<OrderItemDto>? Items { get; set; } // Liste des produits dans la commande
    }
}

