using CleanArchitecture.Entities.Produit;
using CleanArchitecture.FrameworksAndDrivers.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Interfaces
{
    public interface IStockRepository : IGenericRepository<Stock>
    {
        Task<Stock> GetStockByProductIdAsync(int productId);
        bool VerifierDisponibilite(int produitId, int quantite);
        void DeductionStock(int produitId, int quantite);
    }
}
