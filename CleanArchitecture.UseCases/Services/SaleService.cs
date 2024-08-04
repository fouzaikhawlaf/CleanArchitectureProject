using CleanArchitecture.Entities.Sales;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.UseCases.Dtos.SalesDtos;
using CleanArchitecture.UseCases.InterfacesUse;
using CleanArchitecture.UseCases.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Services
{
    public class SaleService : GenericService<Sale, SaleDto, CreateSaleDto, UpdateSaleDto>,ISaleService
    {
        private readonly ISaleRepository _saleRepository;

        public SaleService(ISaleRepository saleRepository) : base(saleRepository)
        {
            _saleRepository = saleRepository;
        }

        protected override SaleDto MapToDto(Sale entity)
        {
            // Map the Sale entity to SaleDto, including clientName and productName
            var dto = entity.MapToDto();
            dto.ClientName = entity.Client?.Name;
            dto.ProductName = entity.Product?.Name;
            return dto;
        }

        protected override Sale MapToEntity(CreateSaleDto createSaleDto)
        {
            return createSaleDto.MapToEntity();
        }

        protected override void MapToEntity(UpdateSaleDto updateSaleDto, Sale sale)
        {
            updateSaleDto.MapToEntity(sale);
        }
        public async Task<IEnumerable<SaleDto>> GetAllSalesAsync()
        {
            var sales = await _saleRepository.GetAllWithDetailsAsync();
            return sales.Select(s => s.MapToDto()).ToList();
        }

        public async Task<IEnumerable<SaleDto>> SearchSalesAsync(string query, string sortBy, bool ascending)
        {
            var sales = await _saleRepository.SearchAsync(query, sortBy, ascending);
            return sales.Select(s => s.MapToDto());
        }

        public async Task<SaleDto> ArchiveSaleAsync(int id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            if (sale != null)
            {
                sale.IsArchived = true;
                await _saleRepository.UpdateAsync(sale);
                return sale.MapToDto();
            }
            else
            {
                throw new KeyNotFoundException($"Sale with Id = {id} not found");
            }
        }

        public async Task<decimal> CalculateTotalAmountAsync(int saleId, int clientId, int productId)
        {
            var sales = await _saleRepository.GetAllWithDetailsAsync();
            var totalAmount = sales.Where(s => s.ClientId == clientId && s.ProductId == productId && s.Id == saleId).Sum(s => s.Amount);

            // Mettre à jour TotalAmount
            var saleToUpdate = await _saleRepository.GetByIdAsync(saleId);
            if (saleToUpdate != null)
            {
                saleToUpdate.TotalAmount = totalAmount;
                await _saleRepository.UpdateAsync(saleToUpdate);
            }

            return totalAmount;
        }


    }
}
