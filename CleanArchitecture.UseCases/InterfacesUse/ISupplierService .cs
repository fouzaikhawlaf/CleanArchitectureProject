using CleanArchitecture.Entities.Sales;
using CleanArchitecture.Entities.Supplier;
using CleanArchitecture.UseCases.Dtos.SupplierDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.InterfacesUse
{
    public  interface ISupplierService : IGenericService<Supplier, SupplierDto, CreateSupplierDto, UpdateSupplierDto>
    {
        Task<IEnumerable<SupplierDto>> SearchAsync(string keyword, string sortBy = "Name", bool ascending = true);
        Task ArchiveSupplierAsync(int id);
        Task<IEnumerable<SupplierDto>> GetArchivedSuppliersAsync();
        Task<byte[]> ExportAllSuppliersToPdfAsync();
    }
}
