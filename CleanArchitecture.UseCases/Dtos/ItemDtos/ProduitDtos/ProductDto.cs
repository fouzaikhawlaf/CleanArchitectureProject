using CleanArchitecture.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.ItemDtos.ProduitDtos
{
    public class ProductDto : ItemDto
    {
        public int ProductID { get; set; }
       
        public string Description { get; set; } = string.Empty; // Initialiser avec une chaîne vide
     
 
        public bool IsArchived { get; set; } = false;
        public ProductType ProductType { get; set; }
        // Ajout des propriétés du frontend
       
        public string PriceType { get; set; } = string.Empty; // Type de prix (HT, TTC, etc.)
        public string Category { get; set; } = string.Empty; // Catégorie du produit
        public string Unit { get; set; } = string.Empty; // Unité de mesure
        public int Quantity { get; set; } // Nouvelle propriété
                                          // Propriété calculée pour le total
        public double Total => Price * Quantity;
    }
}
