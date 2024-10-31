using CleanArchitecture.Entities.Sales;
using CleanArchitecture.FrameworksAndDrivers.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Interfaces
{
    public interface ISaleRepository : IGenericRepository<Sale>
    {

        // Rechercher des ventes par un terme de recherche et trier
        Task<IEnumerable<Sale>> SearchAsync(string query, string sortBy, bool ascending);

        // Archiver une vente spécifique
        Task<Sale?> ArchiveSale(int saleId);

        // Récupérer toutes les ventes associées à un client spécifique (OrderClient)
        Task<IEnumerable<Sale>> GetSalesByOrderClientAsync(int orderClientId);

        // Récupérer les ventes sur une période donnée
        Task<IEnumerable<Sale>> GetSalesByDateRangeAsync(DateTime startDate, DateTime endDate);

        // Récupérer les ventes par client
        Task<IEnumerable<Sale>> GetSalesByClientIdAsync(int clientId);

        // Rechercher des ventes par un statut de vente
        Task<IEnumerable<Sale>> GetSalesByStatusAsync(string status);


        // Récupérer les ventes par un produit spécifique (nom du produit)
        Task<IEnumerable<Sale>> GetSalesByProductNameAsync(string productName);



    }
    
    
}
