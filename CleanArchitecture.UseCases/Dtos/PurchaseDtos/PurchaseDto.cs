using CleanArchitecture.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.PurchaseDtos
{
    public class PurchaseDto
    {
        public int Id { get; set; } // Identifiant de l'achat
        public int InvoiceId { get; set; } // Référence à la facture générée
        public int SupplierId { get; set; } // Référence au fournisseur
        public string? SupplierName { get; set; } // Nom du fournisseur (pour faciliter l'affichage)
        public int ProductId { get; set; } // Référence au produit
        public string? ProductName { get; set; } // Nom du produit (pour faciliter l'affichage)
        public bool IsArchived { get; set; } // Indique si l'achat est archivé
        public DateTime PurchaseDate { get; set; } // Date de l'achat
        public double TotalAmount { get; set; } // Montant total de l'achat
        public PaymentStatus PaymentStatus { get; set; } // Statut du paiement
    }
}
