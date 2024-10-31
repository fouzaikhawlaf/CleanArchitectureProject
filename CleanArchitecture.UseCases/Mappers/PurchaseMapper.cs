using CleanArchitecture.Entities.Purchases;
using CleanArchitecture.UseCases.Dtos.PurchaseDtos;


namespace CleanArchitecture.UseCases.Mappers
{
    public static class PurchaseMapper
    {
        // Mapper une entité Purchase vers un DTO PurchaseDto
        public static PurchaseDto MapToDto(Purchase purchase)
        {
            if (purchase == null)
                return null;

            return new PurchaseDto
            {
                Id = purchase.Id,
                InvoiceId = purchase.InvoiceId,
                SupplierId = purchase.SupplierId,
                SupplierName = purchase.Supplier?.Name, // Assurez-vous que la relation est chargée
                ProductId = purchase.ProductId,
                ProductName = purchase.Product?.Name, // Assurez-vous que la relation est chargée
                IsArchived = purchase.IsArchived,
                PurchaseDate = purchase.PurchaseDate,
                TotalAmount = purchase.TotalAmount,
                PaymentStatus = purchase.PaymentStatus
            };
        }

        // Mapper un DTO CreatePurchaseDto vers une entité Purchase
        public static Purchase MapToEntity(CreatePurchaseDto createPurchaseDto)
        {
            if (createPurchaseDto == null)
                return null;

            return new Purchase
            {
                InvoiceId = createPurchaseDto.InvoiceId,
                SupplierId = createPurchaseDto.SupplierId,
                ProductId = createPurchaseDto.ProductId,
                TotalAmount = createPurchaseDto.TotalAmount,
                PurchaseDate = createPurchaseDto.PurchaseDate,
                PaymentStatus = createPurchaseDto.PaymentStatus,
                IsArchived = false // Par défaut, un nouvel achat n'est pas archivé
            };
        }
        public static void MapToEntity(UpdatePurchaseDto updatePurchaseDto, Purchase purchase)
        {
            if (updatePurchaseDto == null || purchase == null)
                throw new ArgumentNullException("Les entités ne peuvent pas être nulles");

            purchase.InvoiceId = updatePurchaseDto.InvoiceId;
            purchase.SupplierId = updatePurchaseDto.SupplierId;
            purchase.ProductId = updatePurchaseDto.ProductId;
            purchase.TotalAmount = updatePurchaseDto.TotalAmount;
            purchase.PurchaseDate = updatePurchaseDto.PurchaseDate;
            purchase.PaymentStatus = updatePurchaseDto.PaymentStatus;
            purchase.IsArchived = updatePurchaseDto.IsArchived;
        }


        // Mapper un DTO PurchaseDto vers une entité Purchase
        public static Purchase MapToEntity(PurchaseDto purchaseDto)
        {
            if (purchaseDto == null)
                return null;

            return new Purchase
            {
                Id = purchaseDto.Id,
                InvoiceId = purchaseDto.InvoiceId,
                SupplierId = purchaseDto.SupplierId,
                ProductId = purchaseDto.ProductId,
                TotalAmount = purchaseDto.TotalAmount,
                PurchaseDate = purchaseDto.PurchaseDate,
                PaymentStatus = purchaseDto.PaymentStatus,
                IsArchived = purchaseDto.IsArchived
            };
        }
    }
}
