// OrderClientMapper.cs
using CleanArchitecture.Entities.Orders;
using CleanArchitecture.UseCases.Dtos.OrderDtos;

namespace CleanArchitecture.UseCases.Mappers
{
    public static class OrderClientMapper
    {
        // Map OrderClient entity to OrderClientDto
        public static OrderClientDto MapToDto(this OrderClient orderClient)
        {
            return new OrderClientDto
            {
                Id = orderClient.Id,
                ClientId = orderClient.ClientID,
                ClientName = orderClient.Client?.Name,
                SaleAmount = orderClient.SaleAmount,
                TotalAmount = orderClient.TotalAmount,
                TotalTVA = orderClient.TotalTVA,
                Discount = orderClient.Discount,
                OrderDate = orderClient.OrderDate,
                IsDelivered = orderClient.IsDelivered,
                OrderItems = orderClient.OrderItems.Select(item => new OrderItemDto
                {
                    Id = item.Id,
                    ProductName = item.Product?.Name,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    TVA = item.TVA
                }).ToList()
            };
        }

        // Map CreateOrderClientDto to OrderClient entity
        public static OrderClient MapToEntity(this CreateOrderClientDto createDto)
        {
            return new OrderClient
            {
                ClientID = createDto.ClientId,
                SaleAmount = createDto.SaleAmount,
                TotalAmount = createDto.TotalAmount,
                TotalTVA = createDto.TotalTVA,
                Discount = createDto.Discount,
                OrderDate = createDto.OrderDate,
                OrderItems = createDto.OrderItems.Select(itemDto => new OrderItem
                {
                    ProductId = itemDto.ProductId,
                    Quantity = itemDto.Quantity,
                    Price = itemDto.Price,
                    TVA = itemDto.TVA
                }).ToList()
            };
        }

        // Update OrderClient entity from UpdateOrderClientDto
        public static void MapToEntity(this UpdateOrderClientDto updateDto, OrderClient orderClient)
        {
            orderClient.SaleAmount = updateDto.SaleAmount;
            orderClient.TotalAmount = updateDto.TotalAmount;
            orderClient.TotalTVA = updateDto.TotalTVA;
            orderClient.Discount = updateDto.Discount;
            orderClient.OrderDate = updateDto.OrderDate;

            // Update OrderItems
            orderClient.OrderItems.Clear();
            foreach (var itemDto in updateDto.OrderItems)
            {
                orderClient.OrderItems.Add(new OrderItem
                {
                    ProductId = itemDto.ProductId,
                    Quantity = itemDto.Quantity,
                    Price = itemDto.Price,
                    TVA = itemDto.TVA
                });
            }
        }
    }
}
