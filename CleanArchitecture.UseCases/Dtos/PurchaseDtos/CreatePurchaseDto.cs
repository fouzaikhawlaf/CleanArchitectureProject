using CleanArchitecture.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.PurchaseDtos
{
    public class CreatePurchaseDto
    {
        public int InvoiceId { get; set; } // Référence à la facture générée
        public int SupplierId { get; set; } // Référence au fournisseur
        public int ProductId { get; set; } // Référence au produit
        public double TotalAmount { get; set; } // Montant total de l'achat
        public DateTime PurchaseDate { get; set; } // Date de l'achat
        public PaymentStatus PaymentStatus { get; set; } // Statut du paiement
    }
}
