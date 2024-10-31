using CleanArchitecture.Entities.Produit;
using CleanArchitecture.UseCases.Dtos.ItemDtos.ProduitDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.InterfacesUse
{
    public interface IProductService : IGenericService<Product, ProductDto, CreateProductDto, UpdateProductDto>
    {
        Task<IEnumerable<ProductDto>> SearchProductsAsync(string keyword, string sortBy, bool ascending);
        Task<ProductDto?> ArchiveProductAsync(int productId);
        Task<IEnumerable<ProductDto>> GetArchivedProductsAsync();
        Task<byte[]> ExportProductToPdfAsync(int productId);
        Task<byte[]> ExportAllProductsToPdfAsync();
        Task<IEnumerable<ProductDto>> GetProductsAsync(string sortBy, bool ascending);
    }
    
        
    
}
