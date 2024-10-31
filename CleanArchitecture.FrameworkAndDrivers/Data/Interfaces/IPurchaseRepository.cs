using CleanArchitecture.Entities.Enum;
using CleanArchitecture.Entities.Purchases;
using CleanArchitecture.FrameworksAndDrivers.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Interfaces
{
    public interface IPurchaseRepository :IGenericRepository<Purchase>
    {
        // Rechercher des achats par un terme de recherche et trier
        Task<IEnumerable<Purchase>> SearchAsync(string query, string sortBy, bool ascending);

        // Archiver un achat spécifique
        Task<Purchase?> ArchivePurchase(int purchaseId);

        // Récupérer tous les achats associés à un fournisseur spécifique
        Task<IEnumerable<Purchase>> GetPurchasesBySupplierAsync(int supplierId);

        // Récupérer les achats sur une période donnée
        Task<IEnumerable<Purchase>> GetPurchasesByDateRangeAsync(DateTime startDate, DateTime endDate);

        // Récupérer les achats par produit spécifique
        Task<IEnumerable<Purchase>> GetPurchasesByProductNameAsync(string productName);

        // Récupérer les achats par statut de paiement
        Task<IEnumerable<Purchase>> GetPurchasesByPaymentStatusAsync(PaymentStatus paymentStatus);

        // Récupérer les achats par facture
        Task<IEnumerable<Purchase>> GetPurchasesByInvoiceIdAsync(int invoiceId);

        Task<IEnumerable<Purchase>> GetPurchasesHistoryAsync();
        Task<IEnumerable<Purchase>> GetPurchasesByFiltersAsync(DateTime? startDate, DateTime? endDate, int? supplierId, string productName);
    }
}

