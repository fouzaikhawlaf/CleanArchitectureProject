using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Entities.Orders
{
    public class BonDeReception
    {
        [Key] // Assurez-vous d'avoir cet attribut pour définir la clé primaire
        public int Id { get; set; }
        [ForeignKey("OrderSupplierId")]
        public int OrderSupplierId { get; set; }
        public DateTime ReceivedDate { get; set; }
       
        public OrderSupplier? OrderSupplier { get; set; } // Navigation property
        public string SupplierName { get; set; } = string.Empty;  // Supplier name from OrderSupplier
        public string OrderSupplierName { get; set; } = string.Empty; // Supplier name from OrderSupplier
        public int QuantityReceived { get; set; }
        public bool IsArchived { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsInspected { get; set; }
        public double DiscrepancyAmount { get; set; }
        public bool IsFlaggedForReview { get; set; }
        public DateTime? InspectionDate { get; set; }
        public List<BonDeReceptionItem>? Items { get; set; }
    }

}
