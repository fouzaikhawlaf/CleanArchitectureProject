using CleanArchitecture.Entities.Client;
using CleanArchitecture.Entities.Supplier;
using CleanArchitecture.FrameworksAndDrivers.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FramworkAndDrivers.Data.Interfaces
{
    public interface ISupplierRepository : IGenericRepository<Supplier>
    {
        Task<IEnumerable<Supplier>> SearchAsync(string query, string sortBy, bool ascending);
        Task ArchiveSupplierAsync(int id);
        Task<IEnumerable<Supplier>> GetArchivedSuppliersAsync();
    }
}
