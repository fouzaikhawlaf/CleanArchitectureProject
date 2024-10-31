// Application/Mappers/InvoiceClientMapper.cs
using CleanArchitecture.Entities.Invoices;
using CleanArchitecture.UseCases.Dtos.InvoicesDto;
using CleanArchitecture.UseCases.Dtos.InvoicesDto.InvoiceClientDtos;
using System.Linq;

namespace Application.Mappers
{
    public static class InvoiceClientMapper
    {
        public static InvoiceClientDto MapToDto(InvoiceClient invoice)
        {
            return new InvoiceClientDto
            {
                InvoiceId = invoice.InvoiceId,
                ClientName = invoice.ClientName,
                InvoiceDate = invoice.InvoiceDate,
                LineItems = invoice.Items.Select(item => new InvoiceItemDto
                {
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList(),
                Total = invoice.TotalAmount
            };
        }

        public static InvoiceClient MapToEntity(CreateInvoiceClientDto dto)
        {
            return new InvoiceClient
            {
                ClientName = dto.ClientName,
                InvoiceDate = dto.InvoiceDate,
                Items = dto.LineItems.Select(item => new InvoiceLineItem
                {
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList(),
                TotalAmount = dto.Total // Assure-toi que Total est défini dans CreateInvoiceClientDto
            };
        }

        public static InvoiceClient MapToEntity(UpdateInvoiceClientDto dto, InvoiceClient existingInvoice)
        {
            existingInvoice.ClientName = dto.ClientName;
            existingInvoice.InvoiceDate = dto.InvoiceDate;
            existingInvoice.Items = dto.LineItems.Select(item => new InvoiceLineItem
            {
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                Price = item.Price
            }).ToList();
            existingInvoice.TotalAmount = dto.Total; // Assure-toi que Total est défini dans UpdateInvoiceClientDto

            return existingInvoice;
        }
    }
}
