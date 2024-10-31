using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.InvoicesDto
{
    public class CreateInvoiceSupplierDto
    {
        [Required]
        public string? SupplierName { get; set; }  // Nom du fournisseur

        [Required]
        public int OrderSupplierId { get; set; }  // ID de la commande fournisseur

        [Required]
        public int BonDeReceptionId { get; set; }  // ID du bon de réception

        public DateTime InvoiceDate { get; set; } = DateTime.Now;  // Date de création de la facture

        [Required]
        public double TaxRate { get; set; }  // Taux de taxe applicable

        public double Total { get; set; }  // Taux de taxe applicable
        [Required]
        public List<InvoiceItemDto>?LineItems { get; set; }  // Lignes d'articles de la facture
    }
}
