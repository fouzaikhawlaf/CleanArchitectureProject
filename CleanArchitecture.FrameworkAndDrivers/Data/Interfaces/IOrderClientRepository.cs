using CleanArchitecture.Entities.Orders;
using CleanArchitecture.FrameworksAndDrivers.Data.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Interfaces
{
    public interface IOrderClientRepository : IGenericRepository<OrderClient>
    {
    
        Task ArchiveAsync(int id);

     
        Task<IEnumerable<OrderClient>> SearchAsync(string keyword);

   
        Task<OrderClient> GetOrderWithItemsAsync(int id);



        // Get all OrderClients for a specific client by client Id
        Task<IEnumerable<OrderClient>> GetOrdersByClientIdAsync(int clientId);

        // Confirm an OrderClient
        Task ConfirmOrderClientAsync(int id);
        // Advanced operations
        Task<IEnumerable<OrderClient>> GetArchivedOrdersAsync();
        Task<IEnumerable<OrderClient>> GetPendingOrdersAsync(); // Orders not yet delivered
        Task<IEnumerable<OrderClient>> SearchOrdersAsync(string searchTerm);
        Task SaveChangesAsync(); // Ajoutez cette ligne
    }
}
