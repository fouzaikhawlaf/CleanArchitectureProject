using CleanArchitecture.Entities.Produit;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.FramworkAndDrivers.Data.Interfaces;
using CleanArchitecture.UseCases.Dtos.ProduitDtos;
using CleanArchitecture.UseCases.InterfacesUse;
using CleanArchitecture.UseCases.Mappers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Services
{
    public class ProductService : GenericService<Product, ProductDto, CreateProductDto, UpdateProductDto>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IPdfService _pdfService;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, IPdfService pdfService, ILogger<ProductService> logger)
            : base(productRepository)
        {
            _productRepository = productRepository;
            _pdfService = pdfService;
            _logger = logger;
        }

        protected override ProductDto MapToDto(Product product)
        {
            return product.MapToDto();
        }

        protected override Product MapToEntity(CreateProductDto createDto)
        {
            return createDto.MapToEntity();
        }

        protected override void MapToEntity(UpdateProductDto updateDto, Product product)
        {
            updateDto.MapToEntity(product);
        }

        public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string keyword, string sortBy, bool ascending)
        {
            var products = await _productRepository.SearchAsync(keyword, sortBy, ascending);
            return products.Select(p => p.MapToDto());
        }

        public async Task<ProductDto?> ArchiveProductAsync(int productId)
        {
            var product = await _productRepository.ArchiveProductAsync(productId);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {productId} not found.");
            }
            return product.MapToDto();
        }
        public async Task<IEnumerable<ProductDto>> GetArchivedProductsAsync()
        {
            var archivedProducts = await _productRepository.GetArchivedProductsAsync();
            return archivedProducts.Select(p => p.MapToDto());
        }
        public async Task<byte[]> ExportAllProductsToPdfAsync()
        {
            try
            {
                _logger.LogDebug("Fetching all products from the repository");
                var products = await _productRepository.GetAllAsync();

                if (products == null || !products.Any())
                {
                    _logger.LogWarning("No products found in the repository.");
                    throw new Exception("No products found.");
                }

                _logger.LogDebug("Fetched {ProductCount} products", products.Count);

                var pdfBytes = _pdfService.GenerateProductPdf(products);

                if (pdfBytes == null || pdfBytes.Length == 0)
                {
                    _logger.LogWarning("Generated PDF is empty.");
                    throw new Exception("Generated PDF is empty.");
                }

                return pdfBytes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error exporting all products to PDF");
                throw;
            }
        }


        public async Task<byte[]> ExportProductToPdfAsync(int productId)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(productId);
                if (product == null)
                {
                    _logger.LogWarning("Product with ID {ProductId} not found.", productId);
                    return null;
                }

                var products = new List<Product> { product };
                return _pdfService.GenerateProductPdf(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error exporting product to PDF for product ID {ProductId}", productId);
                throw;
            }
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync(string sortBy, bool ascending)
        {
            var products = await _productRepository.GetProductsAsync(sortBy, ascending);
            return products.Select(p => p.MapToDto());
        }
    }
    
    
}
