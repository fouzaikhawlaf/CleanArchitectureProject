using CleanArchitecture.Entities.Suppliers;
using CleanArchitecture.FrameworksAndDrivers;
using CleanArchitecture.FrameworksAndDrivers.Data.Repository;
using CleanArchitecture.FramworkAndDrivers.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FramworkAndDrivers.Data.Repository
{
    public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
    {
        private readonly AppDbContext _dbContext;

        public SupplierRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Supplier>> SearchAsync(string query, string sortBy, bool ascending)
        {
            var suppliers = _dbContext.Suppliers.AsQueryable();

            if (!string.IsNullOrEmpty(query))
            {
                // Parse query to check if it is a valid double for TotalChiffreDAffaire filtering
                bool isNumeric = double.TryParse(query, out double totalChiffreDAffaire);

                suppliers = suppliers.Where(s =>
                    (s.Name != null && s.Name.Contains(query)) ||
                    (s.Email != null && s.Email.Contains(query)) ||
                    s.Phone.ToString().Contains(query) ||
                    (s.Address != null && s.Address.Contains(query)) ||
                    (s.PaymentTerms.ToString().Contains(query)) ||
                    (s.SupplierType.ToString().Contains(query)) ||
                    (isNumeric && s.TotalChiffreDAffaire == totalChiffreDAffaire));
            }

            // Sorting
            if (ascending)
                suppliers = suppliers.OrderBy(s => EF.Property<object>(s, sortBy));
            else
                suppliers = suppliers.OrderByDescending(s => EF.Property<object>(s, sortBy));

            return await suppliers.ToListAsync();
        }
        public async Task ArchiveSupplierAsync(int id)
        {
            var supplier = await _dbContext.Suppliers.FindAsync(id);
            if (supplier != null)
            {
                supplier.IsArchived = true;
                _dbContext.Suppliers.Update(supplier);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Supplier>> GetArchivedSuppliersAsync()
        {
            return await _dbContext.Suppliers
                .Where(s => s.IsArchived)
                .ToListAsync();
        }
    }
}
