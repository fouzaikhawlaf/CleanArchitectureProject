using CleanArchitecture.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.OrderDtos.OrderSupplier
{
    public class UpdateOrderSupplierDto
    {
        public DateTime ExpectedDeliveryDate { get; set; }
        public double PurchaseAmount { get; set; }
        public double TotalAmount { get; set; }
        public TVAType TVARate { get; set; }
        public double TotalTVA { get; set; }
        public double Promotion { get; set; }
        public OrderState Status { get; set; }
        public bool IsArchived { get; set; }
        public List<UpdateOrderItemDto> ?Items { get; set; } // Liste des produits à mettre à jour

    }

}
