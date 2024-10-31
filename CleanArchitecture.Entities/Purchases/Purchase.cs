using CleanArchitecture.Entities.Enum;
using CleanArchitecture.Entities.Invoices;
using CleanArchitecture.Entities.Produit;
using CleanArchitecture.Entities.Suppliers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Entities.Purchases
{
    public class Purchase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("InvoiceId")]
        public int InvoiceId { get; set; } // Référence à la facture générée
        public InvoiceSupplier? Invoice { get; set; } // Relation avec l'entité Invoice
        [ForeignKey("SupplierId")]
        public int SupplierId { get; set; } // Référence au fournisseur
        public Supplier? Supplier { get; set; } // Relation avec l'entité Supplier
        [ForeignKey("ProductId")]
        public int ProductId { get; set; } // Référence au produit
        public Product? Product { get; set; } // Ajoutez cette ligne
        public bool IsArchived { get; set; } // Ajoutez cette ligne si nécessaire
        public DateTime PurchaseDate { get; set; } // Date de l'achat
        public double TotalAmount { get; set; } // Montant total de l'achat

        public PaymentStatus PaymentStatus { get; set; } // Statut du paiement
    }
}
