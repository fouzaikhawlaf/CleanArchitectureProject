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
        Task<decimal> CalculateTotalAmountAsync(int supplierId, int productId);
        Task<IEnumerable<PurchaseDto>> GetAllPurchasesAsync();

    }
}
