using CleanArchitecture.Entities.Enum;
using CleanArchitecture.Entities.Orders;
using CleanArchitecture.UseCases.Dtos.OrderDtos;
using CleanArchitecture.UseCases.Dtos.OrderDtos.OrderSupplier;
using System.Collections.Generic;

namespace CleanArchitecture.UseCases.Mappers
{
    public static class OrderSupplierMapper
    {
        // Maps a CreateOrderSupplierDto to an OrderSupplier entity
        public static OrderSupplier ToEntity(CreateOrderSupplierDto dto)
        {
            return new OrderSupplier
            {
                SupplierID = dto.SupplierId,
                ExpectedDeliveryDate = dto.ExpectedDeliveryDate,
                PurchaseAmount = dto.PurchaseAmount,
                TotalAmount = dto.TotalAmount,
                TotalTVA = dto.TotalTVA,
                Promotion = dto.Promotion,
                Status = OrderState.Pending, // Default status for a new order
                IsArchived = false, // Default value, you can adjust as needed
                Items = dto.Items.Select(item => new OrderItem
                {
                    // Map OrderItem properties based on CreateOrderItemDto
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    TVA = item.TVA
                }).ToList()
            };
        }

        // Maps an UpdateOrderSupplierDto to an existing OrderSupplier entity
        public static void UpdateEntity(OrderSupplier entity, UpdateOrderSupplierDto dto)
        {
            entity.ExpectedDeliveryDate = dto.ExpectedDeliveryDate;
            entity.PurchaseAmount = dto.PurchaseAmount;
            entity.TotalAmount = dto.TotalAmount;
            entity.TotalTVA = dto.TotalTVA;
            entity.Promotion = dto.Promotion;
            entity.Status = dto.Status;
            entity.IsArchived = dto.IsArchived;

            // Update items if needed
            // You can implement additional logic here to update items based on your requirements
        }

        // Maps an OrderSupplier entity to an OrderSupplierDto
        public static OrderSupplierDto ToDto(OrderSupplier entity)
        {
            return new OrderSupplierDto
            {
                Id = entity.Id,
                SupplierId = entity.SupplierID,
                SupplierName = entity.Supplier.Name,  // Assuming Supplier entity has a Name property
                ExpectedDeliveryDate = entity.ExpectedDeliveryDate,
                PurchaseAmount = entity.PurchaseAmount,
                TotalAmount = entity.TotalAmount,
                TotalTVA = entity.TotalTVA,
                Promotion = entity.Promotion,
                Status = entity.Status,
                IsArchived = entity.IsArchived,
                Items = entity.Items.Select(item => new OrderItemDto
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product?.Name, // Assuming Product entity has a Name property
                    Quantity = item.Quantity,
                    Price = item.Price,
                    TVA = item.TVA
                }).ToList() // Map OrderItem to OrderItemDto
            };
        }

        
       
    }
}
