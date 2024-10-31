using CleanArchitecture.Entities.Orders;
using CleanArchitecture.Entities.Produit;
using CleanArchitecture.UseCases.Dtos.OrderDtos.BonDeReceptionDto;
using System.Collections.Generic;
using System.Linq;

namespace CleanArchitecture.UseCases.Mappers
{
    public static class BonDeReceptionMapper
    {
        public static BonDeReceptionDto ToDto(BonDeReception entity)
        {
            return new BonDeReceptionDto
            {
                Id = entity.Id,
                OrderSupplierId = entity.OrderSupplierId,
                ReceivedDate = entity.ReceivedDate,
                IsConfirmed = entity.IsConfirmed,
                IsArchived = entity.IsArchived,
                IsInspected = entity.IsInspected,
                IsAccepted = entity.IsAccepted,
                DiscrepancyAmount = entity.DiscrepancyAmount,
                IsFlaggedForReview = entity.IsFlaggedForReview,
                InspectionDate = entity.InspectionDate,
                Items = entity.Items.Select(item => new BonDeReceptionItemDto
                {
                    Id = item.Item.Id, // Supposant que chaque item a une propriété `Item` qui contient un Id
                    ItemId = item.Item.Id,
                    ReceivedQuantity = item.ReceivedQuantity,
                    UnitPrice = item.Item.Price // Supposant que l'item a une propriété `UnitPrice`
                }).ToList()
            };
        }

        public static BonDeReception ToEntity(CreateBonDeReceptionDto dto)
        {
            return new BonDeReception
            {
                OrderSupplierId = dto.OrderSupplierId,
                ReceivedDate = dto.ReceivedDate,
                IsConfirmed = dto.IsConfirmed,
                IsArchived = dto.IsArchived,
                IsInspected = dto.IsInspected,
                IsAccepted = dto.IsAccepted,
                DiscrepancyAmount = dto.DiscrepancyAmount,
                Items = dto.Items.Select(itemDto => new BonDeReceptionItem
                {
                    Item = new Product // Utilisez une sous-classe concrète de Item
                    {
                        Id = itemDto.ItemId,
                        // Vous pouvez ajouter d'autres propriétés ici si nécessaire
                        Price = itemDto.UnitPrice // Ajoutez le prix unitaire
                    },
                    ReceivedQuantity = itemDto.ReceivedQuantity
                }).ToList()
            };
        }

        public static BonDeReception UpdateEntity(UpdateBonDeReceptionDto dto, BonDeReception entity)
        {
            entity.ReceivedDate = dto.ReceivedDate;
            entity.IsConfirmed = dto.IsConfirmed;
            entity.IsArchived = dto.IsArchived;
            entity.IsInspected = dto.IsInspected;
            entity.IsAccepted = dto.IsAccepted;
            entity.DiscrepancyAmount = dto.DiscrepancyAmount;

            // Mettre à jour les items
            entity.Items.Clear(); // Vider la liste existante
            entity.Items.AddRange(dto.Items.Select(itemDto => new BonDeReceptionItem
            {
                Item = new Product // Utilisez une sous-classe concrète de Item
                {
                    Id = itemDto.ItemId,
                    Price = itemDto.UnitPrice // Ajoutez le prix unitaire
                },
                ReceivedQuantity = itemDto.ReceivedQuantity
            }));

            return entity;
        }
    }
}

