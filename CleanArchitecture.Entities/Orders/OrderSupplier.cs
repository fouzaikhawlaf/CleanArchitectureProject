using CleanArchitecture.Entities.Suppliers;
using System;
using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;
using CleanArchitecture.Entities.Enum;

namespace CleanArchitecture.Entities.Orders
{
    public class OrderSupplier : Order
    {
       
        [Required]
        [ForeignKey(nameof(Supplier))]
        public int SupplierID { get; set; }

       
        public Supplier? Supplier { get; set; }

        [DataType(DataType.Date)]
        public DateTime ExpectedDeliveryDate { get; set; }

     
        public double PurchaseAmount { get; set; }

        public double TotalAmount { get; set; }

        
        public double TotalTVA { get; set; }

        public double Promotion { get; set; }
        public OrderState Status { get; set; }
        public bool IsArchived { get; set; }
        public bool IsDelivered { get; set; }
        public string? SupplierName { get; set; }  // Supplier name from OrderSupplier
        public ICollection<BonDeReception> BonDeReceptions { get; set; } = new List<BonDeReception>();
        public ICollection<OrderItem>? Items { get; set; }
    }
}

