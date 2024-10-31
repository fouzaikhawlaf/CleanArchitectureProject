using CleanArchitecture.Entities.Enum;
using CleanArchitecture.Entities.Orders;
using CleanArchitecture.UseCases.Dtos.OrderDtos.OrderSupplier;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.InterfacesUse
{
    public interface IOrderSupplierService : IGenericService<OrderSupplier, OrderSupplierDto, CreateOrderSupplierDto, UpdateOrderSupplierDto>
    {
        // Retrieve all orders associated with a specific supplier
        Task<IEnumerable<OrderSupplierDto>> GetOrdersBySupplierAsync(int supplierId);

        // Search for orders based on a search term
        Task<IEnumerable<OrderSupplierDto>> SearchOrdersAsync(string searchTerm);

        // Retrieve all archived orders
        Task<IEnumerable<OrderSupplierDto>> GetArchivedOrdersAsync();

        // Archive a specific order
        Task ArchiveOrderAsync(int orderId);

        // Unarchive a specific order
        Task UnarchiveOrderAsync(int orderId);

        // Reject a specific order
        Task RejectOrderAsync(int orderId);

        // Generate a PDF for a specific order
        Task<byte[]> GenerateOrderPdfAsync(IEnumerable<int> orderIds);

        // Confirm a specific order
        Task ConfirmOrderAsync(int orderId);

        // Retrieve orders based on their status
        Task<IEnumerable<OrderSupplierDto>> GetOrdersByStatusAsync(OrderState status);

        // Retrieve orders pending approval
        Task<IEnumerable<OrderSupplierDto>> GetOrdersPendingApprovalAsync();

        // Approve a specific order
        Task<OrderSupplierDto> ApproveOrderAsync(int orderId);

        // Set the expected delivery date for a specific order
        Task SetExpectedDeliveryDateAsync(int orderId, DateTime deliveryDate);

        // Retrieve a specific order by ID
        Task<OrderSupplierDto> GetOrderByIdAsync(int orderId);
        Task<OrderSupplierDto> CreateOrderSupplierAsync(CreateOrderSupplierDto dto);
    }
}
