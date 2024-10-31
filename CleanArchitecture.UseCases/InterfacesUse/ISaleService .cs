using CleanArchitecture.Entities.Invoices;
using CleanArchitecture.Entities.Sales;
using CleanArchitecture.UseCases.Dtos.SalesDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.InterfacesUse
{
    public interface ISaleService : IGenericService<Sale, SaleDto, CreateSaleDto, UpdateSaleDto>
    {
        Task<IEnumerable<SaleDto>> SearchSalesAsync(string query, string sortBy, bool ascending);
        Task<SaleDto> ArchiveSaleAsync(int id);



        /// <summary>
        /// Enregistre une vente à partir de la facture fournie.
        /// </summary>
        /// <param name="invoice">La facture à partir de laquelle la vente est enregistrée.</param>
        /// <returns>Une tâche représentant l'opération asynchrone.</returns>
        Task RegisterSale(InvoiceClient invoice);

        /// <summary>
        /// Récupère l'historique des ventes.
        /// </summary>
        /// <returns>Une liste d'objets SaleDto représentant les ventes.</returns>
        Task<IEnumerable<SaleDto>> GetSalesHistoryAsync();

        /// <summary>
        /// Récupère les ventes en fonction des filtres spécifiés.
        /// </summary>
        /// <param name="startDate">Date de début pour le filtrage.</param>
        /// <param name="endDate">Date de fin pour le filtrage.</param>
        /// <param name="clientId">ID du client pour le filtrage.</param>
        /// <param name="productName">Nom du produit pour le filtrage.</param>
        /// <returns>Une liste d'objets SaleDto représentant les ventes filtrées.</returns>
        Task<IEnumerable<SaleDto>> GetSalesByFiltersAsync(DateTime? startDate, DateTime? endDate, int? clientId, string productName);

        /// <summary>
        /// Récupère toutes les ventes associées à un client spécifique.
        /// </summary>
        /// <param name="orderClientId">ID du client pour récupérer les ventes associées.</param>
        /// <returns>Une liste d'objets SaleDto représentant les ventes associées.</returns>
        Task<IEnumerable<SaleDto>> GetSalesByOrderClientIdAsync(int orderClientId);

        /// <summary>
        /// Récupère les ventes sur une période donnée.
        /// </summary>
        /// <param name="startDate">Date de début pour la période.</param>
        /// <param name="endDate">Date de fin pour la période.</param>
        /// <returns>Une liste d'objets SaleDto représentant les ventes pour la période donnée.</returns>
        Task<IEnumerable<SaleDto>> GetSalesByDateRangeAsync(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Récupère les ventes par client.
        /// </summary>
        /// <param name="clientId">ID du client pour récupérer les ventes.</param>
        /// <returns>Une liste d'objets SaleDto représentant les ventes du client.</returns>
        Task<IEnumerable<SaleDto>> GetSalesByClientIdAsync(int clientId);

        /// <summary>
        /// Récupère les ventes par statut.
        /// </summary>
        /// <param name="status">Statut de la vente pour filtrer.</param>
        /// <returns>Une liste d'objets SaleDto représentant les ventes par statut.</returns>
        Task<IEnumerable<SaleDto>> GetSalesByStatusAsync(string status);

        /// <summary>
        /// Récupère les ventes par nom de produit.
        /// </summary>
        /// <param name="productName">Nom du produit pour récupérer les ventes.</param>
        /// <returns>Une liste d'objets SaleDto représentant les ventes pour le produit donné.</returns>
        Task<IEnumerable<SaleDto>> GetSalesByProductNameAsync(string productName);


        /// <summary>
        /// Exporte les ventes au format CSV.
        /// </summary>
        /// <param name="sales">Liste des ventes à exporter.</param>
        /// <returns>Le chemin du fichier CSV exporté.</returns>
        Task<string> ExportSalesToCsvAsync(IEnumerable<SaleDto> sales);
    }
}

       
    


