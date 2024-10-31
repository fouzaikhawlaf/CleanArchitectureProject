using CleanArchitecture.Entities.Orders;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.FrameworksAndDrivers;
using CleanArchitecture.FrameworksAndDrivers.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Repository
{
    public class DevisRepository :GenericRepository<Devis>, IDevisRepository
    {
        private readonly AppDbContext _dbContext;

        public DevisRepository(AppDbContext context) : base(context)
        {
            _dbContext = context;
        }

        // Return all archived devis (assuming archived means IsAccepted = true)
        public async Task<IEnumerable<Devis>> GetArchivedDevisAsync()
        {
            return await _dbContext.Devises
                .Where(d => d.IsAccepted)
                .ToListAsync();
        }

        // Return all devis that are marked as Brouillon (e.g., IsAccepted = false)
        public async Task<IEnumerable<Devis>> GetBrouillonDevisAsync()
        {
            return await _dbContext.Devises
                .Where(d => !d.IsAccepted)
                .ToListAsync();
        }

        // Search Devis by client name or product name
        public async Task<IEnumerable<Devis>> SearchDevisAsync(string searchTerm)
        {
            return await _dbContext.Devises
                .Include(d => d.Client)
                .Include(d => d.Products)
                .Where(d => d.Client.Name.Contains(searchTerm) || d.Products.Any(p => p.Name.Contains(searchTerm)))
                .ToListAsync();
        }
    }
}
