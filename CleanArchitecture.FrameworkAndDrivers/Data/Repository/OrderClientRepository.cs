using CleanArchitecture.Entities.Orders;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.FrameworksAndDrivers.Data.Repository;
using CleanArchitecture.FrameworksAndDrivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using CleanArchitecture.Entities.Enum;
using iText.Commons.Actions.Contexts;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Repository
{
    public class OrderClientRepository : GenericRepository<OrderClient>, IOrderClientRepository
    {
        private readonly AppDbContext _dbContext;

        public OrderClientRepository(AppDbContext dbContext) :base(dbContext)
        {
            _dbContext = dbContext;
        }

        // Get all OrderClients for a specific client by client Id
        public async Task<IEnumerable<OrderClient>> GetOrdersByClientIdAsync(int clientId)
        {
            return await _dbContext.OrderClients
                .Where(o => o.ClientID == clientId)
                .Include(o => o.Client)
                .Include(o => o.OrderItems) // Include related items
                .ToListAsync();
        }

        // Archive an OrderClient (set IsArchived flag)
        public async Task ArchiveAsync(int id)
        {
            var order = await _dbContext.OrderClients.FindAsync(id);
            if (order != null)
            {
                order.IsArchived = true;
                await _dbContext.SaveChangesAsync();
            }
        }

        // Confirm an OrderClient (set Status as Confirmed)
        public async Task ConfirmOrderClientAsync(int id)
        {
            var order = await _dbContext.OrderClients.FindAsync(id);
            if (order != null)
            {
                order.Status = OrderState.Confirmed; // Assuming OrderState is an enum
                await _dbContext.SaveChangesAsync();
            }
        }

        // Get archived OrderClients
        public async Task<IEnumerable<OrderClient>> GetArchivedOrdersAsync()
        {
            return await _dbContext.OrderClients
                .Where(o => o.IsArchived)
                .ToListAsync();
        }

        // Get pending OrderClients (not yet delivered)
        public async Task<IEnumerable<OrderClient>> GetPendingOrdersAsync()
        {
            return await _dbContext.OrderClients
                .Where(o => !o.IsDelivered)
                .ToListAsync();
        }

        // Search for OrderClients based on a keyword (client name or devis details)
        public async Task<IEnumerable<OrderClient>> SearchOrdersAsync(string searchTerm)
        {
            return await _dbContext.OrderClients
                .Include(o => o.Client)
                .Include(o => o.Devis)
                .Where(o => o.Client.Name.Contains(searchTerm) || o.Devis.Id.ToString().Contains(searchTerm))
                .ToListAsync();
        }

        // Get OrderClient with all related items (OrderItems, Client)
        public async Task<OrderClient> GetOrderWithItemsAsync(int id)
        {
            return await _dbContext.OrderClients
                .Include(o => o.Client)
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        // Search OrderClients (based on custom logic)
        public async Task<IEnumerable<OrderClient>> SearchAsync(string keyword)
        {
            return await _dbContext.OrderClients
                .Include(o => o.Client)
                .Include(o => o.OrderItems)
                .Where(o => o.Client.Name.Contains(keyword) || o.Id.ToString().Contains(keyword))
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync(); // Utilisez le DbContext pour sauvegarder les changements
        }
    }
}