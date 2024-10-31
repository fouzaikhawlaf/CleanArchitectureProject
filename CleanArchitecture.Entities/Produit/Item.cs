using CleanArchitecture.Entities.Enum;
using CleanArchitecture.Entities.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Entities.Produit
{
    public abstract class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public double TaxRate { get; set; } // Taux de TVA en pourcentage
        public TVAType TVARate { get; set; }
        public ICollection<BonDeReceptionItem> BonDeReceptionItems { get; set; } = new List<BonDeReceptionItem>();
        public ICollection<Devis> Devises { get; set; } = new List<Devis>();
    }
}
