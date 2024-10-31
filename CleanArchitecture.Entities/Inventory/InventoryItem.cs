using CleanArchitecture.Entities.Produit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Entities.Inventory
{
    public class InventoryItem
    {
        public int InventoryItemId { get; set; }
        public int ProductId { get; set; } // Foreign key to Product
        public int QuantityInStock { get; set; }
        // This could hold a reference to the Product if necessary
        public Product? Product { get; set; } // Navigation property
    }

}
