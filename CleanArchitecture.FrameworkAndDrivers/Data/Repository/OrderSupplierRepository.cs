using CleanArchitecture.Entities.Enum;
using CleanArchitecture.Entities.Orders;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.FrameworksAndDrivers.Data.Repository;
using CleanArchitecture.FrameworksAndDrivers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Repository
{
    public class OrderSupplierRepository : GenericRepository<OrderSupplier>, IOrderSupplierRepository
    {
        private readonly AppDbContext _context;

        public OrderSupplierRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        // Search orders based on various criteria (e.g., SupplierName, ProductName, etc.)
        public async Task<IEnumerable<OrderSupplier>> SearchOrdersAsync(string searchTerm)
        {
            return await _context.OrderSuppliers
                .Include(o => o.Supplier) // Inclusion de Supplier pour éviter le chargement paresseux
                .Where(o => o.Supplier.Name.Contains(searchTerm) || o.Id.ToString().Contains(searchTerm))
                .ToListAsync();
        }

        // Retrieve all archived orders
        public async Task<IEnumerable<OrderSupplier>> GetArchivedOrdersAsync()
        {
            return await _context.OrderSuppliers
                .Where(o => o.IsArchived)
                .ToListAsync();
        }

        // Archive an order
        public async Task ArchiveOrderAsync(int orderId)
        {
            var order = await _context.OrderSuppliers.FindAsync(orderId);
            if (order != null)
            {
                order.IsArchived = true;
                _context.OrderSuppliers.Update(order);
                await _context.SaveChangesAsync();
            }
        }

        // Unarchive an order
        public async Task UnarchiveOrderAsync(int orderId)
        {
            var order = await _context.OrderSuppliers.FindAsync(orderId);
            if (order != null && order.IsArchived)
            {
                order.IsArchived = false;
                _context.OrderSuppliers.Update(order);
                await _context.SaveChangesAsync();
            }
        }

        // Retrieve orders that need approval (e.g., pending orders)
        public async Task<IEnumerable<OrderSupplier>> GetOrdersPendingApprovalAsync()
        {
            return await _context.OrderSuppliers
                .Where(o => o.Status == OrderState.Pending)
                .ToListAsync();
        }

        // Approve or Confirm an order
        public async Task ApproveOrderAsync(int orderId)
        {
            var order = await _context.OrderSuppliers.FindAsync(orderId);
            if (order != null && order.Status == OrderState.Pending)
            {
                order.Status = OrderState.Confirmed; // Ou OrderState.Approved, selon ta logique
                _context.OrderSuppliers.Update(order);
                await _context.SaveChangesAsync();
            }
        }

        // Reject an order
        public async Task RejectOrderAsync(int orderId)
        {
            var order = await _context.OrderSuppliers.FindAsync(orderId);
            if (order != null && order.Status == OrderState.Pending)
            {
                order.Status = OrderState.Rejected;
                _context.OrderSuppliers.Update(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<OrderSupplier>> GetOrdersByStatusAsync(OrderState status)
        {
            return await _context.OrderSuppliers
                .Where(o => o.Status == status)
                .ToListAsync();
        }

        // Fetch orders by status (OrderState enum)
        public async Task<IEnumerable<OrderSupplier>> GetByStatusAsync(OrderState status)
        {
            return await _context.OrderSuppliers
                .Where(o => o.Status == status)
                .ToListAsync();
        }

        // Retrieve orders by supplier ID
        public async Task<IEnumerable<OrderSupplier>> GetOrdersBySupplierIdAsync(int supplierId)
        {
            return await _context.OrderSuppliers
                .Where(o => o.SupplierID == supplierId)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderSupplier>> GetByIdsAsync(IEnumerable<int> ids)
        {
            // Vérifiez si les IDs sont vides
            if (ids == null || !ids.Any())
            {
                return Enumerable.Empty<OrderSupplier>();
            }

            // Récupérer les commandes fournisseurs par IDs
            var orders = await _context.OrderSuppliers
                .Where(order => ids.Contains(order.Id)) // 'Id' doit être remplacé par la propriété correspondante
                .ToListAsync();

            return orders;
        }
    }
}
