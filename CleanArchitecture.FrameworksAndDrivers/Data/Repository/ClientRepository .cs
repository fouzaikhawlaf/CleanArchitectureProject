using CleanArchitecture.Entities.Client;
using CleanArchitecture.FrameworksAndDrivers;
using CleanArchitecture.FrameworksAndDrivers.Data.Interfaces;
using CleanArchitecture.FrameworksAndDrivers.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
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

        public async Task<IEnumerable<Client>> GetClients(int pageNumber, int pageSize, string sortBy, bool ascending)
        {
            var clients = _context.Clients.AsQueryable();
            clients = SortByProperty(clients, sortBy, ascending);
            return await clients.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<Client>> SearchClients(string query, string sortBy, bool ascending)
        {
            var clients = _context.Clients.AsQueryable();

            if (!string.IsNullOrEmpty(query))
            {
                // Parse query to check if it is a valid double for CreditLimit filtering
                bool isNumeric = double.TryParse(query, out double creditLimit);

                clients = clients.Where(c =>
                    c.Name.Contains(query) ||
                    c.Email.Contains(query) ||
                    c.Phone.Contains(query) ||
                    c.Address.Contains(query) ||
                    c.BillingAddress.Contains(query) ||
                    c.IndustryType.Contains(query) ||
                    c.Tax.Contains(query) ||
                    (isNumeric && c.CreditLimit == creditLimit));
            }

            // Sorting
            if (ascending)
                clients = clients.OrderBy(c => EF.Property<object>(c, sortBy));
            else
                clients = clients.OrderByDescending(c => EF.Property<object>(c, sortBy));

            return await clients.ToListAsync();
        }

        private static IQueryable<T> SortByProperty<T>(IQueryable<T> source, string propertyName, bool ascending)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return source;
            }

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda(property, parameter);

            var methodName = ascending ? "OrderBy" : "OrderByDescending";
            var method = typeof(Queryable).GetMethods()
                .First(m => m.Name == methodName && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), property.Type);

            return (IQueryable<T>)method.Invoke(null, new object[] { source, lambda });
        }
    }
}

