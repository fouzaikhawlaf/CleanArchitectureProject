using CleanArchitecture.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.ItemDtos.ProduitDtos
{
    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public bool IsArchived { get; set; } = false;
        public ProductType ProductType { get; set; }
        // Ajout des propriétés du frontend
        public double TaxRate { get; set; } // Taux de TVA en pourcentage
        public string PriceType { get; set; } = string.Empty; // Type de prix (HT, TTC, etc.)
        public string Category { get; set; } = string.Empty; // Catégorie du produit
        public string Unit { get; set; } = string.Empty; // Unité de mesure
        public int Quantity { get; set; } // Nouvelle propriété
      
    }
}
