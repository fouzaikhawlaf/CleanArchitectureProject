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
    public interface IInvoiceClientService : IGenericService<InvoiceClient, InvoiceClientDto, CreateInvoiceClientDto, UpdateInvoiceClientDto>
    {
        // Méthode pour générer une facture à partir d'une commande client et d'un bon de livraison.
        Task<InvoiceClient> GenerateInvoiceAsync(string orderClientUrl, string bonDeLivraisonUrl);
       // IEnumerable<InvoiceClientDto> SearchInvoices(string searchTerm, DateTime? startDate, DateTime? endDate);

        // Obtenir l'historique des paiements pour une facture donnée
        // IEnumerable<PaymentHistoryDto> GetPaymentHistory(int invoiceId);

        // Exporter les factures au format CSV
        string ExportInvoicesToCsv(IEnumerable<InvoiceClientDto> invoices);

        // Envoyer des rappels automatiques pour les factures impayées
      //  void SendPaymentReminders();

        // Obtenir des statistiques sur les factures
        //InvoiceStatisticsDto GetInvoiceStatistics();

        // Appliquer un modèle spécifique à une facture
      //  void ApplyInvoiceTemplate(InvoiceClient invoice, string templateId);
    }
}
