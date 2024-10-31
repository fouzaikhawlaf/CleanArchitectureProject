using CleanArchitecture.Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Entities.Sales;
using CleanArchitecture.Entities.Purchases;
using CleanArchitecture.Entities.Orders;
using System.Collections;
using CleanArchitecture.Entities.Invoices;

namespace CleanArchitecture.Entities.Produit
{
    public class Product : Item
    {
       
     
       
        public string Description { get; set; } = string.Empty; // Ensure non-nullable
        public int StockQuantity { get; set; }
        public double Price { get; set; }
      
        public ProductType ProductType { get; set; } // Ensure non-nullable
        public bool IsArchived { get; set; } = false;
        public ICollection<Sale> Sales { get; set; } = new List<Sale>(); // Ensure non-nullable
        public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>(); // Ensure non-nullable
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public int Quantity { get; set; } // Add this property
        public virtual ICollection<Stock> ? Stocks { get; set; } // Relation avec les entités Stock

        public ICollection<InvoiceLineItem> ? InvoiceLineItems { get; set; }
    }
}
