using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.InvoicesDto
{
    public class UpdateInvoiceSupplierDto
    {
        [Required]
        public int Id { get; set; }  // ID de la facture à mettre à jour

        [Required]
        public string? SupplierName { get; set; }  // Nom du fournisseur

        [Required]
        public DateTime InvoiceDate { get; set; }  // Date de la facture

        [Required]
        public double TaxRate { get; set; }  // Taux de taxe applicable
        public double Total { get; set; }  // Taux de taxe applicable

        [Required]
        public List<InvoiceItemDto> ? LineItems { get; set; }  // Lignes d'articles de la facture

        public bool IsPaid { get; set; }  // Statut de paiement de la facture
    }
}