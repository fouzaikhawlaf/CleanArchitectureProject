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

        protected override SaleDto MapToDto(Sale sale)
        {
            return sale.MapToDto();
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
            var sales = await _saleRepository.GetAllAsync();
            return sales.Select(s => s.MapToDto());
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

        public async Task<decimal> CalculateTotalAmountAsync(int id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            if (sale == null)
            {
                throw new KeyNotFoundException($"Sale with ID {id} not found.");
            }

            return sale.Amount;
        }

    }
}
