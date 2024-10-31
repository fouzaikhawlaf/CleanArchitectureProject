using CleanArchitecture.Entities.Enum;
using CleanArchitecture.Entities.Orders;
using CleanArchitecture.UseCases.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.InterfacesUse
{
    public interface IOrderClientService : IGenericService<OrderClient, OrderClientDto, CreateOrderClientDto, UpdateOrderClientDto>
    {
        Task ArchiveAsync(int id);

        Task<List<OrderClientDto>> SearchAsync(string keyword);
        Task ConfirmOrderAsync(int id);
        Task<IEnumerable<OrderClientDto>> GetArchivedOrdersAsync();
        Task<IEnumerable<OrderClientDto>> GetPendingOrdersAsync();
        Task<IEnumerable<OrderClientDto>> SearchOrdersAsync(string searchTerm);

        Task ArchiveOrderAsync(int id);

        Task<OrderClientDto> CreateOrderAsync(CreateOrderClientDto createOrderDto);
        Task<OrderClientDto> ValidateOrder(int orderId);
        // Business logic methods
        double CalculateTotalAmount(OrderClientDto orderClient);
        Task<IEnumerable<OrderClientDto>> GetOrdersByClientIdAsync(int clientId);
        Task<byte[]> GenerateOrderPdfAsync(int orderId);
    }
}
