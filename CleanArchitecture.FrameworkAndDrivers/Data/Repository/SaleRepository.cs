using CleanArchitecture.Entities.Sales;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.FrameworksAndDrivers.Data.Repository;
using CleanArchitecture.FrameworksAndDrivers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Repository
{
    public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {
        private readonly AppDbContext _context;

        public SaleRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

     
        public async Task<IEnumerable<Sale>> SearchAsync(string query, string sortBy, bool ascending)
        {
            var sales = _context.Sales.AsQueryable();

            if (!string.IsNullOrEmpty(query))
            {
                sales = sales.Where(s => s.Client.Name.Contains(query) ||
                                         s.Product.Name.Contains(query) ||
                                         s.Amount.ToString().Contains(query));
            }

            sales = ascending ? sales.OrderBy(s => EF.Property<object>(s, sortBy))
                              : sales.OrderByDescending(s => EF.Property<object>(s, sortBy));

            return await sales.ToListAsync();
        }

        public async Task<Sale?> ArchiveSale(int saleId)
        {
            var sale = await _context.Sales.FindAsync(saleId);
            if (sale != null)
            {
                sale.IsArchived = true;
                _context.Sales.Update(sale);
                await _context.SaveChangesAsync();
            }
            return sale;
            //bonjour
        }
    }
    
    
}
