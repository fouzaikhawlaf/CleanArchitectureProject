using CleanArchitecture.Entities.Orders;
using CleanArchitecture.FrameworksAndDrivers.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Interfaces
{
    public interface IDevisRepository : IGenericRepository<Devis>
    {
        // Advanced functions
        Task<IEnumerable<Devis>> GetArchivedDevisAsync();
        Task<IEnumerable<Devis>> GetBrouillonDevisAsync();
        Task<IEnumerable<Devis>> SearchDevisAsync(string searchTerm);
    }
}
