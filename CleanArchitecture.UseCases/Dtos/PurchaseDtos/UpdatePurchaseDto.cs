using CleanArchitecture.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.PurchaseDtos
{
    public class UpdatePurchaseDto
    {
        public int Id { get; set; } // Identifiant de l'achat à mettre à jour
        public int InvoiceId { get; set; } // Référence à la facture générée
        public int SupplierId { get; set; } // Référence au fournisseur
        public int ProductId { get; set; } // Référence au produit
        public double TotalAmount { get; set; } // Montant total de l'achat
        public DateTime PurchaseDate { get; set; } // Date de l'achat
        public PaymentStatus PaymentStatus { get; set; } // Statut du paiement
        public bool IsArchived { get; set; } // Indique si l'achat est archivé

    }
}
