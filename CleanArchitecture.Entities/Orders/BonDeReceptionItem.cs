using CleanArchitecture.Entities.Produit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Entities.Orders
{
    public class BonDeReceptionItem
    {
        [Key] // Assurez-vous d'avoir cet attribut pour définir la clé primaire
        public int Id { get; set; } // Assurez-vous que cette propriété est présente
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public Item Item { get; set; }

        public string? ProductName { get; set; }
        public int ReceivedQuantity { get; set; } // Quantité reçue
        public double UnitPrice { get; set; } // Prix unitaire

        [ForeignKey("BonDeReceptionId")]
        public int BonDeReceptionId { get; set; }
        public BonDeReception? BonDeReception { get; set; }
    }
}
