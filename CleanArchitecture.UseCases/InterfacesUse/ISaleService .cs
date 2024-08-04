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
        Task<decimal> CalculateTotalAmountAsync(int saleId, int clientId, int productId);
        Task<IEnumerable<SaleDto>> GetAllSalesAsync();
        new Task<SaleDto> GetByIdAsync(int id);
    }
}
