using CleanArchitecture.Entities.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Entities.Invoices
{
    public class InvoiceSupplier : Invoice
    {
        [ForeignKey("OrderSupplierId")]
        public int OrderSupplierId { get; set; }  // Foreign key to OrderSupplier
        [ForeignKey("SupplierId")]
        public int SupplierId { get; set; }  // Foreign key to OrderSupplier
        public string? SupplierName { get; set; }  // Ajoute cette propriété
        public OrderSupplier? OrderSupplier { get; set; }  // Navigation property to OrderSupplier
        [ForeignKey("BonDeReceptionId")]
        public int BonDeReceptionId { get; set; }  // Foreign key to BonDeReception
        public BonDeReception? BonDeReception { get; set; }  // Navigation property to BonDeReception
    }
}
