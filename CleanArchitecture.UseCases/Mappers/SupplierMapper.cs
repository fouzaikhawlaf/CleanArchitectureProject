using CleanArchitecture.Entities.Suppliers;
using CleanArchitecture.UseCases.Dtos.SupplierDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Mappers
{
    public static class SupplierMapper
    {
        public static SupplierDto MapToDto(this Supplier supplier)
        {
            return new SupplierDto
            {
                SupplierID = supplier.SupplierId,
                Name = supplier.Name,
                Email = supplier.Email,
                Phone = supplier.Phone,
                Address = supplier.Address,
                IsArchived = supplier.IsArchived,
                PaymentTerms = supplier.PaymentTerms,
                MinimumOrderQuantity = supplier.MinimumOrderQuantity,
                SupplierType = supplier.SupplierType,
                TotalChiffreDAffaire = supplier.TotalChiffreDAffaire
            };
        }

        public static Supplier MapToEntity(this CreateSupplierDto supplierDto)
        {
            return new Supplier
            {
                Name = supplierDto.Name,
                Email = supplierDto.Email,
                Phone = supplierDto.Phone,
                Address = supplierDto.Address,
                IsArchived = false,
                PaymentTerms = supplierDto.PaymentTerms,
                MinimumOrderQuantity = supplierDto.MinimumOrderQuantity,
                SupplierType = supplierDto.SupplierType,
                TotalChiffreDAffaire = supplierDto.TotalChiffreDAffaire
            };
        }

        public static void MapToEntity(this UpdateSupplierDto updateSupplierDto, Supplier supplier)
        {
            supplier.Name = updateSupplierDto.Name;
            supplier.Email = updateSupplierDto.Email;
            supplier.Phone = updateSupplierDto.Phone;
            supplier.Address = updateSupplierDto.Address;
            supplier.IsArchived = updateSupplierDto.IsArchived;
            supplier.PaymentTerms = updateSupplierDto.PaymentTerms;
            supplier.MinimumOrderQuantity = updateSupplierDto.MinimumOrderQuantity;
            supplier.SupplierType = updateSupplierDto.SupplierType;
            supplier.TotalChiffreDAffaire = updateSupplierDto.TotalChiffreDAffaire;
        }
    }

}
