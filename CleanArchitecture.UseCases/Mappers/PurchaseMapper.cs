using CleanArchitecture.Entities.Purchases;
using CleanArchitecture.UseCases.Dtos.PurchaseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Mappers
{

    public static class PurchaseMapper
    {
        public static PurchaseDto MapToDto(this Purchase purchase)
        {
            return new PurchaseDto
            {
                Id = purchase.Id,
                PurchaseDate = purchase.PurchaseDate,
                SupplierId = purchase.SupplierId,
                ProductId = purchase.ProductId,
                Amount = purchase.Amount,
                TotalAmount = purchase.TotalAmount,
                IsArchived = purchase.IsArchived,
                SupplierName = purchase.Supplier?.Name ?? string.Empty, // Ensure non-nullable
                ProductName = purchase.Product?.Name ?? string.Empty // Ensure non-nullable
            };
        }

        public static Purchase MapToEntity(this CreatePurchaseDto purchaseDto)
        {
            return new Purchase
            {
                PurchaseDate = purchaseDto.PurchaseDate,
                SupplierId = purchaseDto.SupplierId,
                ProductId = purchaseDto.ProductId,
                Amount = purchaseDto.Amount,
                TotalAmount = purchaseDto.TotalAmount,
                IsArchived = false // Assuming new purchases are not archived by default
            };
        }

        public static void MapToEntity(this UpdatePurchaseDto purchaseDto, Purchase purchase)
        {
            purchase.PurchaseDate = purchaseDto.PurchaseDate;
            purchase.SupplierId = purchaseDto.SupplierId;
            purchase.ProductId = purchaseDto.ProductId;
            purchase.Amount = purchaseDto.Amount;
            purchase.TotalAmount = purchaseDto.TotalAmount;
            purchase.IsArchived = purchaseDto.IsArchived;
        }
    }
}