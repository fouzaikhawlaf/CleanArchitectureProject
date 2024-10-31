using CleanArchitecture.Entities.Orders;
using CleanArchitecture.FrameworksAndDrivers.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Interfaces
{
    public interface IBonDeReceptionRepository : IGenericRepository<BonDeReception>
    {
        Task<IEnumerable<BonDeReception>> GetByOrderSupplierIdAsync(int orderSupplierId);
        Task<IEnumerable<BonDeReception>> GetAllWithOrderSupplierAsync();
        Task<IEnumerable<BonDeReception>> SearchAsync(string searchTerm);
        Task ConfirmAsync(int id);
        Task ArchiveAsync(int id);
        Task<IEnumerable<BonDeReception>> GetArchivedAsync();
        Task AcceptAsync(int id);
        Task<IEnumerable<BonDeReception>> GetPendingReceptionsAsync();
        Task<IEnumerable<BonDeReception>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalReceivedQuantityBySupplierAsync(int supplierId);
        Task<IEnumerable<BonDeReception>> GetReceptionsWithDiscrepanciesAsync();
        Task MarkAsInspectedAsync(int id);
        Task<IEnumerable<BonDeReception>> GetInspectedReceptionsAsync();
        Task BatchConfirmAsync(IEnumerable<int> receptionIds);
        Task RevertStatusAsync(int id);
        Task FlagForReviewAsync(int id);
        // Ajout des nouvelles méthodes
        Task<IEnumerable<BonDeReception>> GetAllWithOrderSuppliersAsync();
        Task<IEnumerable<BonDeReception>> GetArchivedBonDeReceptionsAsync();






    }
}
