using CleanArchitecture.Entities.Invoices;
using CleanArchitecture.UseCases.Dtos.InvoicesDto;
using CleanArchitecture.UseCases.Dtos.InvoicesDto.InvoiceClientDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.InterfacesUse
{
    public interface IInvoiceSupplierService : IGenericService<InvoiceSupplier, InvoiceSupplierDto, CreateInvoiceSupplierDto, UpdateInvoiceSupplierDto>
    {
        // Méthode pour générer une facture à partir d'une commande fournisseur et d'un bon de réception.
        Task<InvoiceSupplier> GenerateInvoiceAsync(string orderSupplierUrl, string bonDeReceptionUrl);

        // Méthode pour rechercher des factures en fonction de critères de recherche
        IEnumerable<InvoiceSupplierDto> SearchInvoices(string searchTerm, DateTime? startDate, DateTime? endDate);



        // Exporter les factures au format CSV
        string ExportInvoicesToCsv(IEnumerable<InvoiceSupplierDto> invoices);

        // Envoyer des rappels automatiques pour les factures impayées
        // void SendPaymentReminders();

        // Obtenir des statistiques sur les factures
        // InvoiceStatisticsDto GetInvoiceStatistics();

        // Appliquer un modèle spécifique à une facture
        // void ApplyInvoiceTemplate(InvoiceSupplier invoice, string templateId);


        // Obtenir l'historique des paiements pour une facture donnée
        //  IEnumerable<PaymentHistoryDto> GetPaymentHistory(int invoiceId);
    }
}
