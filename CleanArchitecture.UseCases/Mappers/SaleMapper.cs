using CleanArchitecture.Entities.Sales;
using CleanArchitecture.UseCases.Dtos.SalesDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Mappers
{
    public static class SaleMapper
    {
        public static SaleDto MapToDto(this Sale sale)
        {
            return new SaleDto
            {
                Id = sale.Id,
                SaleDate = sale.SaleDate,
                ClientId = sale.ClientId,
                ProductId = sale.ProductId,
                ClientName = sale.Client?.Name ?? string.Empty, // Ajout de ClientName
                ProductName = sale.Product?.Name ?? string.Empty, // Ajout de ProductName
                Amount = sale.Amount,
                IsArchived = sale.IsArchived,
                 TotalAmount = 0
            };
        }

        public static Sale MapToEntity(this CreateSaleDto saleDto)
        {
            return new Sale
            {
                SaleDate = saleDto.SaleDate,
                ClientId = saleDto.ClientId,
                ProductId = saleDto.ProductId,
                Amount = saleDto.Amount,
                IsArchived = false // Assuming new sales are not archived by default
            };
        }

        public static void MapToEntity(this UpdateSaleDto saleDto, Sale sale)
        {
            sale.SaleDate = saleDto.SaleDate;
            sale.ClientId = saleDto.ClientId;
            sale.ProductId = saleDto.ProductId;
            sale.Amount = saleDto.Amount;
            sale.IsArchived = saleDto.IsArchived;
        }
    }
}
