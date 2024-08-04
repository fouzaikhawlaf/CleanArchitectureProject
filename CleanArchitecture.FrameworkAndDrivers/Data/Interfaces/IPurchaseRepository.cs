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
        Task<IEnumerable<Purchase>> SearchAsync(string query, string sortBy, bool ascending);
        Task<Purchase?> ArchivePurchase(int purchaseId);
        Task<decimal> CalculateTotalAmountAsync(int supplierId, int productId);
        Task<IEnumerable<Purchase>> GetAllWithDetailsAsync();
    }
}
