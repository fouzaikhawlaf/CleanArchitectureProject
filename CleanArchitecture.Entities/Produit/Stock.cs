using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Entities.Produit
{
    public class Stock
    {
        [Key] // Assurez-vous d'avoir cet attribut pour définir la clé primaire
        public int Id { get; set; } // Cette propriété doit être présente

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public int QuantityAvailable { get; set; }
        public Product? Product { get; set; } // Relation avec l'entité Product
    }

}
