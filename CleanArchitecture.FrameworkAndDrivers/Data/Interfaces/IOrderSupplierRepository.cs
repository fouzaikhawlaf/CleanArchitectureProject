using CleanArchitecture.Entities.Enum;
using CleanArchitecture.Entities.Orders;
using CleanArchitecture.FrameworksAndDrivers.Data.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Interfaces
{
    public interface IOrderSupplierRepository : IGenericRepository<OrderSupplier>
    {
        /// <summary>
        /// Search orders based on various criteria (e.g., SupplierName, ProductName, etc.).
        /// </summary>
        Task<IEnumerable<OrderSupplier>> SearchOrdersAsync(string searchTerm);

        /// <summary>
        /// Retrieve all archived orders.
        /// </summary>
        Task<IEnumerable<OrderSupplier>> GetArchivedOrdersAsync();

        /// <summary>
        /// Archive an order by its ID.
        /// </summary>
        Task ArchiveOrderAsync(int orderId);

        /// <summary>
        /// Unarchive an order by its ID.
        /// </summary>
        Task UnarchiveOrderAsync(int orderId);

        /// <summary>
        /// Retrieve orders that need approval (e.g., pending orders).
        /// </summary>
        Task<IEnumerable<OrderSupplier>> GetOrdersPendingApprovalAsync();

        /// <summary>
        /// Approve an order by its ID.
        /// </summary>
        Task ApproveOrderAsync(int orderId);

        /// <summary>
        /// Reject an order by its ID.
        /// </summary>
        Task RejectOrderAsync(int orderId);

        /// <summary>
        /// Retrieve orders with a specific status.
        /// </summary>
        Task<IEnumerable<OrderSupplier>> GetOrdersByStatusAsync(OrderState status);

     
        /// <summary>
        /// Retrieve orders by supplier ID.
        /// </summary>
        Task<IEnumerable<OrderSupplier>> GetOrdersBySupplierIdAsync(int supplierId);

        Task<IEnumerable<OrderSupplier>> GetByIdsAsync(IEnumerable<int> ids);
    }
}
