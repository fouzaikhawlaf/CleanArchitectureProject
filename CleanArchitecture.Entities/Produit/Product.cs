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

namespace CleanArchitecture.Entities.Produit
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }
        public string Name { get; set; } = string.Empty; // Ensure non-nullable
        public string Description { get; set; } = string.Empty; // Ensure non-nullable
        public decimal Price { get; set; }
        public ProductType ProductType { get; set; } // Ensure non-nullable
        public bool IsArchived { get; set; } = false;
        public ICollection<Sale> Sales { get; set; } = new List<Sale>(); // Ensure non-nullable
        public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>(); // Ensure non-nullable
    }
}
