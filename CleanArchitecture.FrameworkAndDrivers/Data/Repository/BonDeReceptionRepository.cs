using CleanArchitecture.Entities.Orders;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.FrameworksAndDrivers;
using CleanArchitecture.FrameworksAndDrivers.Data.Interfaces;
using CleanArchitecture.FrameworksAndDrivers.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Repository
{
    public class BonDeReceptionRepository : GenericRepository<BonDeReception>, IBonDeReceptionRepository
    {
        private readonly AppDbContext _context;

        public BonDeReceptionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BonDeReception>> GetByOrderSupplierIdAsync(int orderSupplierId)
        {
            return await _context.BonDeReceptions
                .Where(b => b.OrderSupplierId == orderSupplierId)
                .Include(b => b.OrderSupplier)
                .ToListAsync();
        }

        public async Task<IEnumerable<BonDeReception>> GetAllWithOrderSupplierAsync()
        {
            return await _context.BonDeReceptions
                .Include(b => b.OrderSupplier)
                .ToListAsync();
        }

        public async Task<IEnumerable<BonDeReception>> SearchAsync(string searchTerm)
        {
            return await _context.BonDeReceptions
                .Where(b => b.OrderSupplier.SupplierName.Contains(searchTerm) ||
                            b.ReceivedDate.ToString().Contains(searchTerm) ||
                            b.OrderSupplierId.ToString().Contains(searchTerm))
                .Include(b => b.OrderSupplier)
                .ToListAsync();
        }

        public async Task ConfirmAsync(int id)
        {
            var bonDeReception = await _context.BonDeReceptions.FindAsync(id);
            if (bonDeReception == null) return; // Protection contre null
            bonDeReception.IsConfirmed = true;
            await _context.SaveChangesAsync();
        }

        public async Task ArchiveAsync(int id)
        {
            var bonDeReception = await _context.BonDeReceptions.FindAsync(id);
            if (bonDeReception == null) return;
            bonDeReception.IsArchived = true;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BonDeReception>> GetArchivedAsync()
        {
            return await _context.BonDeReceptions
                .Where(b => b.IsArchived)
                .Include(b => b.OrderSupplier)
                .ToListAsync();
        }

        public async Task AcceptAsync(int id)
        {
            var bonDeReception = await _context.BonDeReceptions.FindAsync(id);
            if (bonDeReception == null) return;
            bonDeReception.IsAccepted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BonDeReception>> GetPendingReceptionsAsync()
        {
            return await _context.BonDeReceptions
                .Where(b => !b.IsConfirmed || !b.IsAccepted)
                .Include(b => b.OrderSupplier)
                .ToListAsync();
        }

        public async Task<IEnumerable<BonDeReception>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.BonDeReceptions
                .Where(b => b.ReceivedDate >= startDate && b.ReceivedDate <= endDate)
                .Include(b => b.OrderSupplier)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalReceivedQuantityBySupplierAsync(int supplierId)
        {
            return await _context.BonDeReceptions
                .Where(b => b.OrderSupplier.SupplierID == supplierId)
                .SumAsync(b => b.QuantityReceived);
        }

        public async Task<IEnumerable<BonDeReception>> GetReceptionsWithDiscrepanciesAsync()
        {
            return await _context.BonDeReceptions
                .Where(b => b.QuantityReceived != b.OrderSupplier.Items.Sum(item => item.Quantity))
                .Include(b => b.OrderSupplier)
                .ToListAsync();
        }



        public async Task MarkAsInspectedAsync(int id)
        {
            var bonDeReception = await _context.BonDeReceptions.FindAsync(id);
            if (bonDeReception == null) return;
            bonDeReception.IsInspected = true;
            bonDeReception.InspectionDate = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BonDeReception>> GetInspectedReceptionsAsync()
        {
            return await _context.BonDeReceptions
                .Where(b => b.IsInspected)
                .Include(b => b.OrderSupplier)
                .ToListAsync();
        }

        public async Task BatchConfirmAsync(IEnumerable<int> receptionIds)
        {
            var receptions = await _context.BonDeReceptions
                .Where(b => receptionIds.Contains(b.Id))
                .ToListAsync();

            if (!receptions.Any()) return;

            foreach (var reception in receptions)
            {
                reception.IsConfirmed = true;
            }

            await _context.SaveChangesAsync();
        }

        public async Task RevertStatusAsync(int id)
        {
            var bonDeReception = await _context.BonDeReceptions.FindAsync(id);
            if (bonDeReception == null) return;
            bonDeReception.IsConfirmed = false;
            bonDeReception.IsAccepted = false;
            await _context.SaveChangesAsync();
        }

        public async Task FlagForReviewAsync(int id)
        {
            var bonDeReception = await _context.BonDeReceptions.FindAsync(id);
            if (bonDeReception == null) return;
            bonDeReception.IsFlaggedForReview = true;
            await _context.SaveChangesAsync();
        }



        public async Task<IEnumerable<BonDeReception>> GetAllWithOrderSuppliersAsync()
        {
            // Exemple d'implémentation, à ajuster selon votre contexte
            return await Task.FromResult(_context.BonDeReceptions
                .Include(br => br.OrderSupplier) // Assurez-vous que l'inclusion de l'entité est correcte
                .ToList());
        }

        public async Task<IEnumerable<BonDeReception>> GetArchivedBonDeReceptionsAsync()
        {
            // Exemple d'implémentation, à ajuster selon votre contexte
            return await Task.FromResult(_context.BonDeReceptions
                .Where(br => br.IsArchived)
                .ToList());
        }
    }
}
