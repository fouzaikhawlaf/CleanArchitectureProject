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
        
        Task<IEnumerable<Sale>> SearchAsync(string query, string sortBy, bool ascending);
        Task<Sale?> ArchiveSale(int saleId);
    }
    
    
}
