using CleanArchitecture.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.OrderDtos.OrderSupplier
{
    public class OrderSupplierDto
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; } = string.Empty;
        public DateTime ExpectedDeliveryDate { get; set; }
        public double PurchaseAmount { get; set; }
        public double TotalAmount { get; set; }
        public double TotalTVA { get; set; }
        public double Promotion { get; set; }
        public OrderState Status { get; set; }
        public bool IsArchived { get; set; }
        public List<OrderItemDto> ?Items { get; set; } // Liste des produits dans la commande
    }

}
