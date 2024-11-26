using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Entities.Orders.DeliveryNotes
{
    // Domain/Entities/DeliveryNote.cs
    public class DeliveryNote
    {
        [Key] // Assurez-vous d'avoir cet attribut pour définir la clé primaire
        public int Id { get; set; }
        [ForeignKey("OrderClientId")]
        public int OrderClientId { get; set; }

        // Navigation property to OrderClient
        public OrderClient? OrderClient { get; set; }

        public DateTime DeliveryDate { get; set; }
        public string DeliveryDetails { get; set; } = string.Empty;
        public bool IsDelivered { get; set; }
        public bool IsArchived { get; set; } // New field for archiving status
        public List<DeliveryItem> DeliveryNoteItems { get; set; } = new List<DeliveryItem>(); // Liste des articles livrés
        public string Status { get; set; } = string.Empty; // Statut du bon de livraison (ex: "En cours", "Livré", etc.)
    }

}
