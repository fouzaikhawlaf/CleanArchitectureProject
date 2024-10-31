using CleanArchitecture.Entities.Purchases;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.FrameworksAndDrivers.Data.Repository;
using CleanArchitecture.FrameworksAndDrivers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Entities.Enum;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Repository
{
    public class PurchaseRepository : GenericRepository<Purchase>, IPurchaseRepository
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
                .Include(p => p.Invoice) // Assurez-vous que cette propriété existe
                .ToListAsync();
        }

        public async Task<IEnumerable<Purchase>> SearchAsync(string query, string sortBy, bool ascending)
        {
            var purchases = _context.Purchases
                .Include(p => p.Supplier)
                .Include(p => p.Invoice) // Assurez-vous que cette propriété existe
                .AsQueryable();

            if (!string.IsNullOrEmpty(query))
            {
                purchases = purchases.Where(p =>
                    p.Supplier.Name.Contains(query) ||
                    p.Invoice.InvoiceId.ToString().Contains(query) || // Ajuster selon vos propriétés
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
                // Supposons que vous ayez une propriété IsArchived dans votre classe Purchase
                purchase.IsArchived = true;
                _context.Purchases.Update(purchase);
                await _context.SaveChangesAsync();
            }
            return purchase;
        }

        public async Task<IEnumerable<Purchase>> GetPurchasesBySupplierAsync(int supplierId)
        {
            return await _context.Purchases
                .Where(p => p.SupplierId == supplierId)
                .Include(p => p.Supplier)
                .ToListAsync();
        }

        public async Task<IEnumerable<Purchase>> GetPurchasesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Purchases
                .Where(p => p.PurchaseDate >= startDate && p.PurchaseDate <= endDate)
                .Include(p => p.Supplier)
                .ToListAsync();
        }

        public async Task<IEnumerable<Purchase>> GetPurchasesByProductNameAsync(string productName)
        {
            return await _context.Purchases
                .Include(p => p.Supplier)
                .Include(p => p.Invoice)
                .Where(p => p.Product.Name.Contains(productName)) // Assurez-vous que cette propriété existe
                .ToListAsync();
        }

        public async Task<IEnumerable<Purchase>> GetPurchasesByPaymentStatusAsync(PaymentStatus paymentStatus)
        {
            return await _context.Purchases
                .Where(p => p.PaymentStatus == paymentStatus)
                .Include(p => p.Supplier)
                .ToListAsync();
        }

        public async Task<IEnumerable<Purchase>> GetPurchasesByInvoiceIdAsync(int invoiceId)
        {
            return await _context.Purchases
                .Where(p => p.InvoiceId == invoiceId)
                .Include(p => p.Supplier)
                .ToListAsync();
        }


        public async Task<IEnumerable<Purchase>> GetPurchasesHistoryAsync()
        {
            return await _context.Purchases.OrderByDescending(p => p.PurchaseDate).ToListAsync();
        }


        public async Task<IEnumerable<Purchase>> GetPurchasesByFiltersAsync(DateTime? startDate, DateTime? endDate, int? supplierId, string productName)
        {
            // Implémentation de la méthode avec des filtres
            var query = _context.Purchases.AsQueryable();

            if (startDate.HasValue)
            {
                query = query.Where(p => p.PurchaseDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(p => p.PurchaseDate <= endDate.Value);
            }

            if (supplierId.HasValue)
            {
                query = query.Where(p => p.SupplierId == supplierId.Value);
            }

            if (!string.IsNullOrEmpty(productName))
            {
                query = query.Where(p => p.Product.Name.Contains(productName));
            }

            return await query.ToListAsync();
        }



    }
}
