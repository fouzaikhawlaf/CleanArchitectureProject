// Application/Mappers/InvoiceSupplierMapper.cs
using CleanArchitecture.Entities.Invoices;
using CleanArchitecture.UseCases.Dtos.InvoicesDto;
using System.Linq;

namespace Application.Mappers
{
    public static class InvoiceSupplierMapper
    {
        public static InvoiceSupplierDto MapToDto(InvoiceSupplier invoice)
        {
            return new InvoiceSupplierDto
            {
                InvoiceId = invoice.InvoiceId,
                SupplierName = invoice.SupplierName,
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

        public static InvoiceSupplier MapToEntity(CreateInvoiceSupplierDto dto)
        {
            return new InvoiceSupplier
            {
                SupplierName = dto.SupplierName,
                InvoiceDate = dto.InvoiceDate,
                Items = dto.LineItems.Select(item => new InvoiceLineItem
                {
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList(),
                TotalAmount = dto.Total // Assure-toi que Total est défini dans CreateInvoiceSupplierDto
            };
        }

        public static InvoiceSupplier MapToEntity(UpdateInvoiceSupplierDto dto, InvoiceSupplier existingInvoice)
        {
            existingInvoice.SupplierName = dto.SupplierName;
            existingInvoice.InvoiceDate = dto.InvoiceDate;
            existingInvoice.Items = dto.LineItems.Select(item => new InvoiceLineItem
            {
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                Price = item.Price
            }).ToList();
            existingInvoice.TotalAmount = dto.Total; // Assure-toi que Total est défini dans UpdateInvoiceSupplierDto

            return existingInvoice;
        }
    }
}
