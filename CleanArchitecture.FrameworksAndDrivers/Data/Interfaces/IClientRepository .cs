
using CleanArchitecture.Entities.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworksAndDrivers.Data.Interfaces
{
    public interface IClientRepository : IGenericRepository<Client>
    {

        Task<Client?> ArchiveClient(int clientId);
        Task<IEnumerable<Client>> GetClients(int pageNumber, int pageSize, string sortBy, bool ascending);
        Task<IEnumerable<Client>> SearchClients(string query, string sortBy, bool ascending);
    }
}

