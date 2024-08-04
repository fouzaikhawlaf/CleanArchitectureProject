using CleanArchitecture.Entities.Suppliers;
using CleanArchitecture.FramworkAndDrivers.Data.Interfaces;
using CleanArchitecture.UseCases.Dtos.SupplierDtos;
using CleanArchitecture.UseCases.InterfacesUse;
using CleanArchitecture.UseCases.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Services
{
    public class SupplierService : GenericService<Supplier, SupplierDto, CreateSupplierDto, UpdateSupplierDto>, ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IPdfService _pdfService;

        public SupplierService(ISupplierRepository supplierRepository, IPdfService pdfService) : base(supplierRepository)
        {
            _supplierRepository = supplierRepository;
            _pdfService = pdfService;
        }

        protected override SupplierDto MapToDto(Supplier entity)
        {
            return entity.MapToDto();
        }

        protected override Supplier MapToEntity(CreateSupplierDto createDto)
        {
            return createDto.MapToEntity();
        }

        protected override void MapToEntity(UpdateSupplierDto updateDto, Supplier entity)
        {
            updateDto.MapToEntity(entity);
        }

        public async Task<IEnumerable<SupplierDto>> SearchAsync(string keyword, string sortBy = "Name", bool ascending = true)
        {
            var suppliers = await _supplierRepository.SearchAsync(keyword, sortBy, ascending);
            return suppliers.Select(s => s.MapToDto());
        }

        public async Task ArchiveSupplierAsync(int id)
        {
            var supplier = await _supplierRepository.GetByIdAsync(id);
            if (supplier == null)
            {
                throw new KeyNotFoundException($"Supplier with ID {id} not found.");
            }

            supplier.IsArchived = true;
            await _supplierRepository.UpdateAsync(supplier);
        }

        public async Task<IEnumerable<SupplierDto>> GetArchivedSuppliersAsync()
        {
            var archivedSuppliers = await _supplierRepository.GetArchivedSuppliersAsync();
            return archivedSuppliers.Select(s => s.MapToDto());
        }

        public async Task<byte[]> ExportAllSuppliersToPdfAsync()
        {
            var suppliers = await _supplierRepository.GetAllAsync();
            return _pdfService.GenerateSupplierPdf(suppliers);
        }
    }
}
