using CleanArchitecture.Entities.Enum;
using CleanArchitecture.Entities.Invoices;
using CleanArchitecture.Entities.Purchases;
using CleanArchitecture.UseCases.Dtos.PurchaseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.InterfacesUse
{
    public interface IPurchaseService : IGenericService<Purchase, PurchaseDto, CreatePurchaseDto, UpdatePurchaseDto>
    {
        Task<IEnumerable<PurchaseDto>> SearchPurchasesAsync(string query, string sortBy, bool ascending);
        Task<PurchaseDto> ArchivePurchaseAsync(int id);



        /// <summary>
        /// Enregistre un achat à partir des détails fournis.
        /// </summary>
        /// <param name="purchaseDto">Les détails de l'achat à enregistrer.</param>
        /// <returns>Une tâche représentant l'opération asynchrone.</returns>
        Task RegisterPurchaseAsync(InvoiceSupplier invoice);

        /// <summary>
        /// Récupère l'historique des achats.
        /// </summary>
        /// <returns>Une liste d'objets PurchaseDto représentant les achats.</returns>
        Task<IEnumerable<PurchaseDto>> GetPurchasesHistoryAsync();

        /// <summary>
        /// Récupère les achats en fonction des filtres spécifiés.
        /// </summary>
        /// <param name="startDate">Date de début pour le filtrage.</param>
        /// <param name="endDate">Date de fin pour le filtrage.</param>
        /// <param name="supplierId">ID du fournisseur pour le filtrage.</param>
        /// <param name="productName">Nom du produit pour le filtrage.</param>
        /// <returns>Une liste d'objets PurchaseDto représentant les achats filtrés.</returns>
        Task<IEnumerable<PurchaseDto>> GetPurchasesByFiltersAsync(DateTime? startDate, DateTime? endDate, int? supplierId, string productName);

        /// <summary>
        /// Récupère tous les achats associés à un fournisseur spécifique.
        /// </summary>
        /// <param name="supplierId">ID du fournisseur pour récupérer les achats associés.</param>
        /// <returns>Une liste d'objets PurchaseDto représentant les achats associés.</returns>
        Task<IEnumerable<PurchaseDto>> GetPurchasesBySupplierIdAsync(int supplierId);

        /// <summary>
        /// Récupère les achats sur une période donnée.
        /// </summary>
        /// <param name="startDate">Date de début pour la période.</param>
        /// <param name="endDate">Date de fin pour la période.</param>
        /// <returns>Une liste d'objets PurchaseDto représentant les achats pour la période donnée.</returns>
        Task<IEnumerable<PurchaseDto>> GetPurchasesByDateRangeAsync(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Récupère les achats par statut de paiement.
        /// </summary>
        /// <param name="paymentStatus">Statut de paiement pour filtrer.</param>
        /// <returns>Une liste d'objets PurchaseDto représentant les achats par statut de paiement.</returns>
        Task<IEnumerable<PurchaseDto>> GetPurchasesByPaymentStatusAsync(PaymentStatus paymentStatus);

        /// <summary>
        /// Récupère les achats par nom de produit.
        /// </summary>
        /// <param name="productName">Nom du produit pour récupérer les achats.</param>
        /// <returns>Une liste d'objets PurchaseDto représentant les achats pour le produit donné.</returns>
        Task<IEnumerable<PurchaseDto>> GetPurchasesByProductNameAsync(string productName);

        /// <summary>
        /// Exporte les achats au format CSV.
        /// </summary>
        /// <param name="purchases">Liste des achats à exporter.</param>
        /// <returns>Le chemin du fichier CSV exporté.</returns>
        Task<string> ExportPurchasesToCsvAsync(IEnumerable<PurchaseDto> purchases);

    }
}
