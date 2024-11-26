using CleanArchitecture.Entities.Produit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Entities.Clients;
using CleanArchitecture.Entities.Orders;
using CleanArchitecture.Entities.Invoices;

namespace CleanArchitecture.Entities.Sales
{
    public class Sale
    {
        [Key] // Assurez-vous d'avoir cet attribut pour définir la clé primaire
        public int SaleId { get; set; }
        [ForeignKey("InvoiceClient")]// Identifiant unique pour chaque vente
        public int InvoiceClientInvoiceId { get; set; }    // Référence à l'identifiant de la facture
        public int ClientId { get; set; }     // Référence à l'identifiant du client
        public DateTime SaleDate { get; set; }  // Date de la vente
        public double TotalAmount { get; set; } // Montant total de la vente
        public string Status { get; set; } = string.Empty;// Ajoutez cette ligne
        public bool IsArchived { get; set; } // Ajoutez cette ligne si nécessaire
                                             // Relations
      
        public InvoiceClient? Invoice { get; set; }  // Relation avec l'entité facture
        [ForeignKey("ClientId")]
        public Client? Client { get; set; }
        [ForeignKey("ProductId")]// Relation avec l'entité client
        public int ProductId { get; set; }          // Référence à l'identifiant du produit
      
        public Product? Product { get; set; }        // Relation avec l'entité produit
        [NotMapped]
        public string ProductName { get; set; } = string.Empty;// Assurez-vous que cette propriété existe
    }


}
