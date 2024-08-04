using CleanArchitecture.Entities.Purchases;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.UseCases.Dtos.PurchaseDtos;
using CleanArchitecture.UseCases.InterfacesUse;
using CleanArchitecture.UseCases.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        protected override PurchaseDto MapToDto(Purchase entity)
        {
            var dto = entity.MapToDto();
            dto.SupplierName = entity.Supplier?.Name;
            dto.ProductName = entity.Product?.Name;
            return dto;
        }

        protected override Purchase MapToEntity(CreatePurchaseDto createPurchaseDto)
        {
            return createPurchaseDto.MapToEntity();
        }

        protected override void MapToEntity(UpdatePurchaseDto updatePurchaseDto, Purchase purchase)
        {
            updatePurchaseDto.MapToEntity(purchase);
        }

        public async Task<IEnumerable<PurchaseDto>> GetAllPurchasesAsync()
        {
            var purchases = await _purchaseRepository.GetAllWithDetailsAsync();
            return purchases.Select(p => p.MapToDto()).ToList();
        }

        public async Task<IEnumerable<PurchaseDto>> SearchPurchasesAsync(string query, string sortBy, bool ascending)
        {
            var purchases = await _purchaseRepository.SearchAsync(query, sortBy, ascending);
            return purchases.Select(p => p.MapToDto());
        }

        public async Task<PurchaseDto> ArchivePurchaseAsync(int id)
        {
            var purchase = await _purchaseRepository.GetByIdAsync(id);
            if (purchase != null)
            {
                purchase.IsArchived = true;
                await _purchaseRepository.UpdateAsync(purchase);
                return purchase.MapToDto();
            }
            else
            {
                throw new KeyNotFoundException($"Purchase with Id = {id} not found");
            }
        }

        public async Task<decimal> CalculateTotalAmountAsync(int supplierId, int productId)
        {
            var purchases = await _purchaseRepository.GetAllWithDetailsAsync();
            var totalAmount = purchases.Where(p => p.SupplierId == supplierId && p.ProductId == productId).Sum(p => p.TotalAmount);

            return totalAmount;
        }
    }
}

