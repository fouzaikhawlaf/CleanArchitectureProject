using CleanArchitecture.Entities.Clients;
using CleanArchitecture.FrameworksAndDrivers;
using CleanArchitecture.FrameworksAndDrivers.Data.Interfaces;
using CleanArchitecture.FrameworksAndDrivers.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Data.Repository
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Client?> ArchiveClient(int clientId)
        {
            var client = await GetByIdAsync(clientId);
            if (client != null)
            {
                client.IsArchived = true;
                await UpdateAsync(client);
            }
            return client;
        }

        public async Task<IEnumerable<Client>> GetClients()
        {
            var clients = _context.Clients.AsQueryable();
            return await clients.ToListAsync();
        }

        public async Task<IEnumerable<Client>> SearchClients(string query, string sortBy, bool ascending)
        {
            var clients = _context.Clients.AsQueryable();

            if (!string.IsNullOrEmpty(query))
            {
                // Parse query to check if it is a valid double for CreditLimit filtering
                bool isNumeric = double.TryParse(query, out double creditLimit);

                clients = clients.Where(c =>
                    (c.Name != null && c.Name.Contains(query)) ||
                    (c.Email != null && c.Email.Contains(query)) ||
                    c.Phone.Contains(query) ||
                    (c.Address != null && c.Address.Contains(query)) ||
                    (c.BillingAddress != null && c.BillingAddress.Contains(query)) ||
                    (c.IndustryType != null && c.IndustryType.Contains(query)) ||
                    (c.Tax != null && c.Tax.Contains(query)) ||
                    (isNumeric && c.CreditLimit == creditLimit));
            }

            // Sorting
            if (ascending)
                clients = clients.OrderBy(c => EF.Property<object>(c, sortBy));
            else
                clients = clients.OrderByDescending(c => EF.Property<object>(c, sortBy));

            return await clients.ToListAsync();
        }
    }
}
