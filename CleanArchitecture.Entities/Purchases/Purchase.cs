using CleanArchitecture.Entities.Produit;
using CleanArchitecture.Entities.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Entities.Purchases
{
    public class Purchase
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }= null!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; } // Nouveau champ
        public bool IsArchived { get; set; } = false;
        public string SupplierName { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
    }
}
