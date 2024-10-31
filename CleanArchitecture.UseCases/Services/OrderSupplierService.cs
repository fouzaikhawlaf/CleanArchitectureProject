using CleanArchitecture.Entities.Enum;
using CleanArchitecture.Entities.Orders;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.FramworkAndDrivers.Data.Interfaces;
using CleanArchitecture.UseCases.Dtos.OrderDtos.OrderSupplier;
using CleanArchitecture.UseCases.InterfacesUse;
using CleanArchitecture.UseCases.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Services
{
    public class OrderSupplierService : GenericService<OrderSupplier, OrderSupplierDto, CreateOrderSupplierDto, UpdateOrderSupplierDto>, IOrderSupplierService
    {
        private readonly IOrderSupplierRepository _orderSupplierRepository;
        private readonly IPdfService _pdfService;

        public OrderSupplierService(IOrderSupplierRepository orderSupplierRepository, IPdfService pdfService)
            : base(orderSupplierRepository)
        {
            _orderSupplierRepository = orderSupplierRepository;
            _pdfService = pdfService;
        }

        // Create a new order supplier with VAT and total amount calculation
        public async Task<OrderSupplierDto> CreateOrderSupplierAsync(CreateOrderSupplierDto dto)
        {
            var orderSupplier = MapToEntity(dto);

            double tvaRate = (int)dto.TVARate / 100.0; // Convert TVAType enum to percentage
            double totalVAT = dto.PurchaseAmount * tvaRate;
            double totalAmount = dto.PurchaseAmount + totalVAT;

            orderSupplier.TotalTVA = totalVAT;
            orderSupplier.TotalAmount = totalAmount;

            await _orderSupplierRepository.AddAsync(orderSupplier);

            return MapToDto(orderSupplier);
        }

        // Retrieve all orders associated with a specific supplier
        public async Task<IEnumerable<OrderSupplierDto>> GetOrdersBySupplierAsync(int supplierId)
        {
            var orders = await _orderSupplierRepository.GetOrdersBySupplierIdAsync(supplierId);
            return orders.Select(OrderSupplierMapper.ToDto);
        }

        // Search orders based on various criteria
        public async Task<IEnumerable<OrderSupplierDto>> SearchOrdersAsync(string searchTerm)
        {
            var orders = await _orderSupplierRepository.SearchOrdersAsync(searchTerm);
            return orders.Select(OrderSupplierMapper.ToDto);
        }

        // Retrieve all archived orders
        public async Task<IEnumerable<OrderSupplierDto>> GetArchivedOrdersAsync()
        {
            var orders = await _orderSupplierRepository.GetArchivedOrdersAsync();
            return orders.Select(OrderSupplierMapper.ToDto);
        }

        // Archive an order
        public async Task ArchiveOrderAsync(int orderId)
        {
            await _orderSupplierRepository.ArchiveOrderAsync(orderId);
        }

        // Unarchive an order
        public async Task UnarchiveOrderAsync(int orderId)
        {
            await _orderSupplierRepository.UnarchiveOrderAsync(orderId);
        }

        // Approve an order
        public async Task<OrderSupplierDto> ApproveOrderAsync(int orderId)
        {
            var order = await _orderSupplierRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException("Order not found.");
            }

            order.Status = OrderState.Approved;
            await _orderSupplierRepository.UpdateAsync(order);

            return OrderSupplierMapper.ToDto(order);
        }

        // Reject an order
        public async Task RejectOrderAsync(int orderId)
        {
            var order = await _orderSupplierRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException("Order not found.");
            }

            order.Status = OrderState.Rejected;
            await _orderSupplierRepository.UpdateAsync(order);
        }


        public async Task<byte[]> GenerateOrderPdfAsync(IEnumerable<int> orderIds)
        {
            // Vérifiez si l'ID de la commande est fourni
            if (orderIds == null || !orderIds.Any())
            {
                throw new ArgumentException("No order IDs provided.");
            }

            // Récupérez les commandes à partir de l'ID fourni
            var orders = await _orderSupplierRepository.GetByIdsAsync(orderIds);

            // Vérifiez si des commandes ont été trouvées
            if (orders == null || !orders.Any())
            {
                throw new KeyNotFoundException("No orders found for the provided IDs.");
            }

            // Générez le PDF en fonction du nombre de commandes récupérées
            return _pdfService.GenerateOrderSuppliersPdf(orders);
        }


        // Calculate the total amount for a specific order
        public async Task<double> CalculateTotalAmountAsync(int orderId)
        {
            var order = await _orderSupplierRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException("Order not found.");
            }

            double totalAmount = (double)(order.PurchaseAmount - order.Promotion + order.TotalTVA);
            return totalAmount;
        }

        // Calculate the total VAT (TVA) for a specific order
        public async Task<double> CalculateTotalVATAsync(int orderId)
        {
            var order = await _orderSupplierRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException("Order not found.");
            }

            return (double)order.TotalTVA;
        }

        // Set expected delivery date for an order
        public async Task SetExpectedDeliveryDateAsync(int orderId, DateTime deliveryDate)
        {
            var order = await _orderSupplierRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException("Order not found.");
            }

            order.ExpectedDeliveryDate = deliveryDate;
            await _orderSupplierRepository.UpdateAsync(order);
        }

        // Retrieve orders by status
        public async Task<IEnumerable<OrderSupplierDto>> GetOrdersByStatusAsync(OrderState status)
        {
            var orders = await _orderSupplierRepository.GetOrdersByStatusAsync(status);
            return orders.Select(OrderSupplierMapper.ToDto);
        }


        public async Task<OrderSupplierDto> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderSupplierRepository.GetByIdAsync(orderId);
            return OrderSupplierMapper.ToDto(order);
        }

        public async Task ConfirmOrderAsync(int orderId)
        {
            var order = await _orderSupplierRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException("Order not found.");
            }

            order.Status = OrderState.Confirmed; // Assurez-vous que le statut "Confirmed" existe dans OrderState
            await _orderSupplierRepository.UpdateAsync(order);
        }











        // Retrieve orders pending approval
        public async Task<IEnumerable<OrderSupplierDto>> GetOrdersPendingApprovalAsync()
        {
            var orders = await _orderSupplierRepository.GetOrdersPendingApprovalAsync();
            return orders.Select(OrderSupplierMapper.ToDto);
        }

        protected override OrderSupplier MapToEntity(CreateOrderSupplierDto dto)
        {
            return OrderSupplierMapper.ToEntity(dto);
        }

        protected override void MapToEntity(UpdateOrderSupplierDto dto, OrderSupplier entity)
        {
            OrderSupplierMapper.UpdateEntity(entity, dto);
        }

        protected override OrderSupplierDto MapToDto(OrderSupplier entity)
        {
            return OrderSupplierMapper.ToDto(entity);
        }
    }
}
