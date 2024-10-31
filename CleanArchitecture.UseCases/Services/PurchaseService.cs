using CleanArchitecture.Entities.Enum;
using CleanArchitecture.Entities.Invoices;
using CleanArchitecture.Entities.Purchases;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.UseCases.Dtos.PurchaseDtos;
using CleanArchitecture.UseCases.InterfacesUse;
using CleanArchitecture.UseCases.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Services
{
    public class PurchaseService : GenericService<Purchase, PurchaseDto, CreatePurchaseDto, UpdatePurchaseDto>, IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseService(IPurchaseRepository purchaseRepository) : base(purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

     
        protected override Purchase MapToEntity(CreatePurchaseDto createPurchaseDto)
        {
            return PurchaseMapper.MapToEntity(createPurchaseDto); // Utilisation de la méthode du mapper
        }

        protected override void MapToEntity(UpdatePurchaseDto updatePurchaseDto, Purchase purchase)
        {
            PurchaseMapper.MapToEntity(updatePurchaseDto, purchase); // Utilisation de la méthode du mapper
        }


        protected override PurchaseDto MapToDto(Purchase purchase)
        {
            return PurchaseMapper.MapToDto(purchase);
        }



        public async Task<IEnumerable<PurchaseDto>> GetPurchasesHistoryAsync()
        {
            var purchases = await _purchaseRepository.GetPurchasesHistoryAsync();
            return purchases.Select(p => MapToDto(p)).ToList();
        }

        public async Task<IEnumerable<PurchaseDto>> SearchPurchasesAsync(string query, string sortBy, bool ascending)
        {
            var purchases = await _purchaseRepository.SearchAsync(query, sortBy, ascending);
            return purchases.Select(p => MapToDto(p)).ToList(); // Conversion en liste
        }

        public async Task<PurchaseDto> ArchivePurchaseAsync(int id)
        {
            var purchase = await _purchaseRepository.GetByIdAsync(id);
            if (purchase != null)
            {
                purchase.IsArchived = true;
                await _purchaseRepository.UpdateAsync(purchase);
                return MapToDto(purchase); // Utilisation de la méthode de mappage
            }
            else
            {
                throw new KeyNotFoundException($"Purchase with Id = {id} not found");
            }
        }



        public async Task RegisterPurchaseAsync(InvoiceSupplier invoice)
        {
            if (invoice == null)
            {
                throw new ArgumentNullException(nameof(invoice), "Invoice cannot be null.");
            }

            var purchase = new Purchase
            {
                SupplierId = invoice.SupplierId, // Assurez-vous que le SupplierId est défini dans l'invoice
                ProductId = invoice.Items.FirstOrDefault()?.ProductId ?? 0, // Utilise 0 si ProductId est null
                TotalAmount = invoice.TotalAmount,
                PurchaseDate = DateTime.UtcNow, // Date de l'achat actuelle
                IsArchived = false // Définir par défaut comme non archivé
            };

            // Enregistrer l'achat
            await _purchaseRepository.AddAsync(purchase);
        }



        // Si nécessaire, vous pouvez également gérer d'autres opérations après l'enregistrement
    




    public async Task<IEnumerable<PurchaseDto>> GetPurchasesByFiltersAsync(DateTime? startDate, DateTime? endDate, int? supplierId, string productName)
        {
            var purchases = await _purchaseRepository.GetPurchasesByFiltersAsync(startDate, endDate, supplierId, productName);
            return purchases.Select(p => MapToDto(p)).ToList();
        }

        public async Task<IEnumerable<PurchaseDto>> GetPurchasesBySupplierIdAsync(int supplierId)
        {
            var purchases = await _purchaseRepository.GetPurchasesBySupplierAsync(supplierId);
            return purchases.Select(p => MapToDto(p)).ToList();
        }

        public async Task<IEnumerable<PurchaseDto>> GetPurchasesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var purchases = await _purchaseRepository.GetPurchasesByDateRangeAsync(startDate, endDate);
            return purchases.Select(p => MapToDto(p)).ToList();
        }

        public async Task<IEnumerable<PurchaseDto>> GetPurchasesByPaymentStatusAsync(PaymentStatus paymentStatus)
        {
            var purchases = await _purchaseRepository.GetPurchasesByPaymentStatusAsync(paymentStatus);
            return purchases.Select(p => MapToDto(p)).ToList();
        }

        public async Task<IEnumerable<PurchaseDto>> GetPurchasesByProductNameAsync(string productName)
        {
            var purchases = await _purchaseRepository.GetPurchasesByProductNameAsync(productName);
            return purchases.Select(p => MapToDto(p)).ToList();
        }

        public async Task<string> ExportPurchasesToCsvAsync(IEnumerable<PurchaseDto> purchases)
        {
            // Implémenter la logique pour exporter en CSV
            throw new NotImplementedException("CSV export not implemented yet.");
        }
    }
}
