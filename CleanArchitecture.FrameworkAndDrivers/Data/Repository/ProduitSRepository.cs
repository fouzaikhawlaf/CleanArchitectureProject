using CleanArchitecture.Entities.Produit;
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
    public class ProduitSRepository:GenericRepository<Service>, IProduitSRepository
    {

        private readonly AppDbContext _context;
        public ProduitSRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Service>> GetAllAvailableServicesAsync()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<Service> GetServiceByIdAsync(int serviceId)
        {
            return await _context.Services.FindAsync(serviceId);
        }

        // Implémentations des méthodes génériques
        public async Task AddAsync(Service entity)
        {
            await _context.Services.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Service> ArchiveServiceAsync(int serviceId)
        {
            var service = await _context.Services.FindAsync(serviceId);
            if (service != null)
            {
                // Marquer le service comme archivé
                service.IsArchived = true; // Assurez-vous que cette propriété existe dans votre classe Service
                await _context.SaveChangesAsync();
            }
            return service; // Retourne le service archivé
        }

        public async Task<IEnumerable<Service>> SearchServicesAsync(string searchTerm)
        {
            return await _context.Services
                .Where(s => s.Name.Contains(searchTerm) || s.Description.Contains(searchTerm) && !s.IsArchived)
                .ToListAsync();
        }



    }
}
