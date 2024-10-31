using CleanArchitecture.Entities.Orders;
using CleanArchitecture.UseCases.Dtos.OrderDtos.BonDeReceptionDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.InterfacesUse
{
    public interface IBonDeReceptionService : IGenericService<BonDeReception, BonDeReceptionDto, CreateBonDeReceptionDto, UpdateBonDeReceptionDto>
    {
        Task ConfirmBonDeReceptionAsync(int id);
        Task ArchiveBonDeReceptionAsync(int id);
        Task MarkAsInspectedAsync(int id);
        Task<IEnumerable<BonDeReceptionDto>> SearchBonDeReceptionsAsync(string searchTerm);
        Task<IEnumerable<BonDeReceptionDto>> GetPendingReceptionsAsync();
        Task HandleReceptionDiscrepanciesAsync(int id, double discrepancyAmount);
        Task RevertBonDeReceptionAsync(int id);

        // Nouvelle méthode pour créer un bon de réception à partir d'une commande fournisseur
        Task CreateBonDeReceptionFromOrderSupplierAsync(int orderSupplierId);

        // Nouvelle méthode pour obtenir tous les bons de réception avec leurs commandes associées
        Task<IEnumerable<BonDeReceptionDto>> GetAllWithOrderSuppliersAsync();

        // Nouvelle méthode pour obtenir les bons de réception archivés
        Task<IEnumerable<BonDeReceptionDto>> GetArchivedBonDeReceptionsAsync();
    }
}
