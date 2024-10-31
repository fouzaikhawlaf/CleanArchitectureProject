using CleanArchitecture.Entities.Sales;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.FrameworksAndDrivers.Data.Repository;
using CleanArchitecture.FrameworksAndDrivers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Repository
{
    public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {
        private readonly AppDbContext _context;

        // Pass the context to the base repository class and initialize it locally
        public SaleRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sale>> SearchAsync(string query, string sortBy, bool ascending)
        {
            var sales = _context.Sales.AsQueryable();

            if (!string.IsNullOrEmpty(query))
            {
                sales = sales.Where(sale => sale.Invoice.ClientId.ToString().Contains(query) ||
                                             sale.TotalAmount.ToString().Contains(query) ||
                                             sale.SaleDate.ToString().Contains(query));
            }

            // Tri des ventes
            sales = sortBy.ToLower() switch
            {
                "totalamount" => ascending ? sales.OrderBy(sale => sale.TotalAmount) : sales.OrderByDescending(sale => sale.TotalAmount),
                "saledate" => ascending ? sales.OrderBy(sale => sale.SaleDate) : sales.OrderByDescending(sale => sale.SaleDate),
                _ => sales.OrderBy(sale => sale.InvoiceClientInvoiceId) // Tri par défaut
            };

            return await sales.ToListAsync();
        }

        public async Task<Sale?> ArchiveSale(int saleId)
        {
            var sale = await _context.Sales.FindAsync(saleId);
            if (sale != null)
            {
                // Logique pour archiver (ex : changer le statut)
                sale.IsArchived = true;
                await _context.SaveChangesAsync();
            }
            return sale;
        }

        public async Task<IEnumerable<Sale>> GetSalesByOrderClientAsync(int orderClientId)
        {
            return await _context.Sales
                .Where(sale => sale.Invoice.ClientId == orderClientId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Sale>> GetSalesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Sales
                .Where(sale => sale.SaleDate >= startDate && sale.SaleDate <= endDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Sale>> GetSalesByClientIdAsync(int clientId)
        {
            return await _context.Sales
                .Where(sale => sale.ClientId == clientId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Sale>> GetSalesByStatusAsync(string status)
        {
            return await _context.Sales
                .Where(sale => sale.Status.Equals(status, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }

        public async Task<IEnumerable<Sale>> GetSalesByProductNameAsync(string productName)
        {
            return await _context.Sales
                .Where(sale => sale.Invoice.Items.Any(item => item.ProductName.Contains(productName)))
                .ToListAsync();
        }

    }
}
