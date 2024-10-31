using CleanArchitecture.Entities.Produit;
using CleanArchitecture.UseCases.Dtos.ItemDtos.ProduitDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto MapToDto(this Product product)
        {
            return new ProductDto
            {
                ProductID = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ProductType = product.ProductType,
                IsArchived = product.IsArchived
            };
        }

        public static Product MapToEntity(this CreateProductDto productDto)
        {
            return new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                ProductType = productDto.ProductType,
                IsArchived = false
            };
        }

        public static void MapToEntity(this UpdateProductDto updateProductDto, Product product)
        {
            product.Name = updateProductDto.Name;
            product.Description = updateProductDto.Description;
            product.Price = updateProductDto.Price;
            product.ProductType = updateProductDto.ProductType;
            product.IsArchived = updateProductDto.IsArchived;
        }
    }
}
