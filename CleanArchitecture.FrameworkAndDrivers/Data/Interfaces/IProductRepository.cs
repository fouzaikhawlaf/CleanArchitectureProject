using CleanArchitecture.Entities.Produit;
using CleanArchitecture.FrameworksAndDrivers.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> SearchAsync(string keyword, string sortBy, bool ascending);
        Task<Product?> ArchiveProductAsync(int productId);
        Task<IEnumerable<Product>> GetArchivedProductsAsync();
        Task<IEnumerable<Product>> GetProductsAsync(string sortBy, bool ascending);
    }
    
    
}
