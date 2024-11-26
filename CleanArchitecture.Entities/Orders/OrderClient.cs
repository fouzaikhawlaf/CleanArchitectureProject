using CleanArchitecture.Entities.Clients;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CleanArchitecture.Entities.Sales;
using CleanArchitecture.Entities.Orders.DeliveryNotes;

namespace CleanArchitecture.Entities.Orders
{


    public class OrderClient : Order
    {

        [ForeignKey(nameof(Client))]
        public int ClientID { get; set; }
        public Client? Client { get; set; }
        public string ClientName { get; set; } = string.Empty;// Ajout du nom du client spécifiquement pour la commande
        public double SaleAmount { get; set; }
        public double TotalAmount { get; set; }
        public double TotalTVA { get; set; }
        public double Discount { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow; // Définir la date de commande par défaut
        public bool IsDelivered { get; set; } = false; // Champ pour l'archivage
        [ForeignKey(nameof(Devis))]
        public int DevisId { get; set; } // Changez Id à DevisId
        // Ajoutez une navigation à Devis si nécessaire
        public Devis? Devis { get; set; }
        public ICollection<DeliveryNote> DeliveryNotes { get; set; } = new List<DeliveryNote>();
        // Navigation property to Sale

    }
}