using CleanArchitecture.Entities.Purchases;
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
    public class PurchaseRepository :GenericRepository<Purchase>, IPurchaseRepository
    {

        private readonly AppDbContext _context;

        public PurchaseRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Purchase>> GetAllWithDetailsAsync()
        {
            return await _context.Purchases
                .Include(p => p.Supplier)
                .Include(p => p.Product)
                .ToListAsync();
        }

        public async Task<IEnumerable<Purchase>> SearchAsync(string query, string sortBy, bool ascending)
        {
            var purchases = _context.Purchases
                .Include(p => p.Supplier)
                .Include(p => p.Product)
                .AsQueryable();

            if (!string.IsNullOrEmpty(query))
            {
                purchases = purchases.Where(p =>
                    p.Supplier.Name.Contains(query) ||
                    p.Product.Name.Contains(query) ||
                    p.Amount.ToString().Contains(query) ||
                    p.PurchaseDate.ToString().Contains(query) ||
                    p.TotalAmount.ToString().Contains(query));
            }

            purchases = ascending
                ? purchases.OrderBy(p => EF.Property<object>(p, sortBy))
                : purchases.OrderByDescending(p => EF.Property<object>(p, sortBy));

            return await purchases.ToListAsync();
        }


        public async Task<Purchase?> ArchivePurchase(int purchaseId)
        {
            var purchase = await _context.Purchases.FindAsync(purchaseId);
            if (purchase != null)
            {
                purchase.IsArchived = true;
                _context.Purchases.Update(purchase);
                await _context.SaveChangesAsync();
            }
            return purchase;
        }

        public async Task<decimal> CalculateTotalAmountAsync(int supplierId, int productId)
        {
            var purchases = await _context.Purchases
                .Where(p => p.SupplierId == supplierId && p.ProductId == productId)
                .ToListAsync();

            return purchases.Sum(p => p.Amount);
        }
    }



}

