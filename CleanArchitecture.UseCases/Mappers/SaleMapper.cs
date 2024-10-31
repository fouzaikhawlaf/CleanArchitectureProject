using CleanArchitecture.Entities.Sales;
using CleanArchitecture.UseCases.Dtos.SalesDtos;

public static class SaleMapper
{
    // Convertir Sale en SaleDto
    public static SaleDto ToDto(Sale sale)
    {
        if (sale == null)
            return null;

        return new SaleDto
        {
            Id = sale.SaleId,
            InvoiceId = sale.InvoiceClientInvoiceId,
            ClientId = sale.ClientId,
            SaleDate = sale.SaleDate,
            TotalAmount = sale.TotalAmount,
            Status = sale.Status, // Assurez-vous que ces propriétés existent dans votre entité
            IsArchived = sale.IsArchived // Assurez-vous que ces propriétés existent dans votre entité
        };
    }

    // Convertir SaleDto en Sale
    public static Sale FromDto(SaleDto saleDto)
    {
        if (saleDto == null)
            return null;

        return new Sale
        {
            SaleId = saleDto.Id,
            InvoiceClientInvoiceId = saleDto.InvoiceId,
            ClientId = saleDto.ClientId,
            SaleDate = saleDto.SaleDate,
            TotalAmount = saleDto.TotalAmount,
            Status = saleDto.Status,
            IsArchived = saleDto.IsArchived
        };
    }

    // Convertir CreateSaleDto en Sale
    public static Sale FromCreateDto(CreateSaleDto createSaleDto)
    {
        if (createSaleDto == null)
            return null;

        return new Sale
        {
            InvoiceClientInvoiceId = createSaleDto.InvoiceId,
            ClientId = createSaleDto.ClientId,
            SaleDate = createSaleDto.SaleDate,
            TotalAmount = createSaleDto.TotalAmount
        };
    }

    // Convertir UpdateSaleDto en Sale
    public static Sale FromUpdateDto(UpdateSaleDto updateSaleDto)
    {
        if (updateSaleDto == null)
            return null;

        return new Sale
        {
            SaleId = updateSaleDto.Id,
            InvoiceClientInvoiceId = updateSaleDto.InvoiceId,
            ClientId = updateSaleDto.ClientId,
            SaleDate = updateSaleDto.SaleDate,
            TotalAmount = updateSaleDto.TotalAmount,
            Status = updateSaleDto.Status,
            IsArchived = updateSaleDto.IsArchived
        };
    }
}
