using CleanArchitecture.Entities.Clients;
using CleanArchitecture.Entities.Enum;
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
    // Domain/Entities/Devis.cs
    public class Devis
    {
        [Key] // Assurez-vous d'avoir cet attribut pour définir la clé primaire
        public int Id { get; set; }
        [ForeignKey("ClientId")]
        public int ClientId { get; set; }
        public Client? Client { get; set; }
        // Navigation vers OrderClient (One-to-One)
        public OrderClient? OrderClient { get; set; }
        public List<Product> ?Products { get; set; }
        public double TotalAmount { get; set; }
        public TVAType TVARate { get; set; }
        public double TVA { get; set; }
        public double TotalTVA { get; set; }
        public DevisStatus Status { get; set; } = DevisStatus.Pending; // Default to Pending
        public bool IsAccepted { get; set; }
    }

}
