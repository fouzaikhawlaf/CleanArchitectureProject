using CleanArchitecture.Entities.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Entities.Produit;

namespace CleanArchitecture.Entities.Invoices
{
    public class InvoiceLineItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }  // Foreign key to OrderSupplier
        public Product? Product { get; set;  }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
        [ForeignKey("Invoice")]
        public int InvoiceId { get; set; }  // Foreign key to OrderSupplier
        public Invoice? Invoice { get; set;  }
    }
}
