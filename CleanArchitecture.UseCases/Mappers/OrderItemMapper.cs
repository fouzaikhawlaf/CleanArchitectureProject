using CleanArchitecture.Entities.Orders;
using CleanArchitecture.UseCases.Dtos.OrderDtos;
using CleanArchitecture.UseCases.Dtos.OrderDtos.OrderSupplier;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanArchitecture.UseCases.Mappers
{
    public static class OrderItemMapper
    {
        public static OrderItemDto MapToDto(this OrderItem orderItem)
        {
            return new OrderItemDto
            {
                Id = orderItem.Id,
                ProductId = orderItem.ProductId,
                ProductName = orderItem.Product?.Name ?? string.Empty, // Récupère le nom du produit
                Quantity = orderItem.Quantity,
                Price = orderItem.Price,
                TVARate = orderItem.TVARate,
                TVA = orderItem.TVA
            };
        }

        public static OrderItem MapToEntity(this CreateOrderItemDto createOrderItemDto)
        {
            return new OrderItem
            {
                ProductId = createOrderItemDto.ProductId,
                Quantity = createOrderItemDto.Quantity,
                Price = createOrderItemDto.Price,
                TVARate = createOrderItemDto.TVARate
            };
        }

        public static void MapToEntity(this UpdateOrderItemDto updateOrderItemDto, OrderItem orderItem)
        {
            orderItem.ProductId = updateOrderItemDto.ProductId;
            orderItem.Quantity = updateOrderItemDto.Quantity;
            orderItem.Price = updateOrderItemDto.Price;
            orderItem.TVARate = updateOrderItemDto.TVARate;
            // Si vous avez une propriété IsArchived ou autre à mettre à jour, ajoutez-la ici
        }

        public static OrderItem MapToEntity(this UpdateOrderItemDto updateOrderItemDto)
        {
            return new OrderItem
            {
                // Mettez ici les propriétés à mapper
                ProductId = updateOrderItemDto.ProductId,
                Quantity = updateOrderItemDto.Quantity,
                Price = updateOrderItemDto.Price,
                // Ajoutez d'autres propriétés si nécessaire
            };
        }
    }
}
