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
        // Map Product entity to ProductDto
        public static ProductDto MapToDto(this Product product)
        {
            return new ProductDto
            {
                ProductID = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ProductType = product.ProductType,
                IsArchived = product.IsArchived,
                TaxRate = product.TaxRate,
                PriceType = product.PriceType,
                Category = product.Category,
                Unit = product.Unit,
               
            };
        }

        // Map CreateProductDto to Product entity
        public static Product MapToEntity(this CreateProductDto productDto)
        {
            return new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                ProductType = productDto.ProductType,
                IsArchived = false,
                TaxRate = productDto.TaxRate,
                PriceType = productDto.PriceType,
                Category = productDto.Category,
                Unit = productDto.Unit
            };
        }

        // Map UpdateProductDto to an existing Product entity
        public static void MapToEntity(this UpdateProductDto updateProductDto, Product product)
        {
            product.Name = updateProductDto.Name;
            product.Description = updateProductDto.Description;
            product.Price = updateProductDto.Price;
            product.ProductType = updateProductDto.ProductType;
            product.IsArchived = updateProductDto.IsArchived;
            product.TaxRate = updateProductDto.TaxRate;
            product.PriceType = updateProductDto.PriceType;
            product.Category = updateProductDto.Category;
            product.Unit = updateProductDto.Unit;
        }
    }
}
