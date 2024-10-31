using CleanArchitecture.Entities.Orders.DeliveryNotes;
using CleanArchitecture.UseCases.Dtos.OrderDtos.DeliveryNoteDto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanArchitecture.UseCases.Mappers
{
    public static class DeliveryNoteMapper
    {
        // Map DeliveryNote to DeliveryNoteDto
        public static DeliveryNoteDto MapToDto(this DeliveryNote deliveryNote)
        {
            return new DeliveryNoteDto
            {
                Id = deliveryNote.Id,
                OrderClientId = deliveryNote.OrderClientId,
                DeliveryDate = deliveryNote.DeliveryDate,
                IsDelivered = deliveryNote.IsDelivered,
                IsArchived = deliveryNote.IsArchived,
                Items = deliveryNote.DeliveryNoteItems?.Select(item => new DeliveryItemDto
                {
                    ProductName = item.Product.Name,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList() // Map each DeliveryNoteItem to DeliveryNoteItemDto
            };
        }

        // Map CreateDeliveryNoteDto to DeliveryNote
        public static DeliveryNote MapToEntity(this CreateDeliveryNoteDto createDto)
        {
            return new DeliveryNote
            {
                OrderClientId = createDto.OrderClientId,
                DeliveryDate = createDto.DeliveryDate,
                IsDelivered = false, // Default value for new deliveries
                IsArchived = false,  // Default value for new deliveries
                DeliveryNoteItems = createDto.Items?.Select(item => new DeliveryItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList() // Map each CreateDeliveryNoteItemDto to DeliveryNoteItem
            };
        }

        // Map UpdateDeliveryNoteDto to an existing DeliveryNote (for updating)
        public static void MapToEntity(this UpdateDeliveryNoteDto updateDto, DeliveryNote deliveryNote)
        {
            deliveryNote.DeliveryDate = updateDto.DeliveryDate;
            deliveryNote.IsDelivered = updateDto.IsDelivered;
            deliveryNote.IsArchived = updateDto.IsArchived;

            // Update the DeliveryNoteItems if provided
            if (updateDto.Items != null)
            {
                deliveryNote.DeliveryNoteItems = updateDto.Items.Select(item => new DeliveryItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList();
            }
        }
    }
}
