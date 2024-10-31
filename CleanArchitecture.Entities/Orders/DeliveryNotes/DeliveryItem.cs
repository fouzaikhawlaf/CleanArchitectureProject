using CleanArchitecture.Entities.Produit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Entities.Orders.DeliveryNotes
{
    public class DeliveryItem
    {
        [Key]
        public int Id { get; set; }
        public int DeliveryNoteId { get; set; } // Foreign key to DeliveryNote
        public DeliveryNote? DeliveryNote { get; set; }
        [ForeignKey("ProductId")]
        public int ProductId { get; set; } // ID of the product
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public virtual Product? Product { get; set; } // Navigation property to Product
    }
}
