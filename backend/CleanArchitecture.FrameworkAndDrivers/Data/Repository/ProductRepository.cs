using CleanArchitecture.Entities.Produit;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.FrameworksAndDrivers;
using CleanArchitecture.FrameworksAndDrivers.Data.Repository;
using CleanArchitecture.FramworkAndDrivers.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<ProductRepository> _logger;
        public ProductRepository(AppDbContext dbContext , ILogger<ProductRepository> logger) : base(dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;

        }

        public async Task<IEnumerable<Product>> SearchAsync(string keyword, string sortBy, bool ascending)
        {
            var products = _dbContext.Products.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                products = products.Where(p => p.Name.Contains(keyword) || p.Description.Contains(keyword));
            }

            products = ascending
                ? products.OrderBy(e => EF.Property<object>(e, sortBy))
                : products.OrderByDescending(e => EF.Property<object>(e, sortBy));

            return await products.ToListAsync();
        }

        public async Task<Product?> ArchiveProductAsync(int productId)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            if (product != null)
            {
                product.IsArchived = true;
                _dbContext.Products.Update(product);
                await _dbContext.SaveChangesAsync();
            }
            return product;
        }
        public async Task<IEnumerable<Product>> GetArchivedProductsAsync()
        {
            return await _dbContext.Products
                .Where(p => p.IsArchived)
                .ToListAsync();
        }



        public async Task<IEnumerable<Product>> GetProductsAsync(string sortBy, bool ascending)
        {
            var products = _dbContext.Products.AsQueryable();

            products = ascending
                ? products.OrderBy(e => EF.Property<object>(e, sortBy))
                : products.OrderByDescending(e => EF.Property<object>(e, sortBy));

            return await products.ToListAsync();
        }
    }
}