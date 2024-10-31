using CleanArchitecture.Entities.Produit;
using CleanArchitecture.FrameworksAndDrivers.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Interfaces
{
    public interface IProduitSRepository : IGenericRepository<Service>
    {
        Task<IEnumerable<Service>> GetAllAvailableServicesAsync(); // Pour obtenir tous les services disponibles
        Task<Service> GetServiceByIdAsync(int serviceId); // Pour obtenir un service spécifique
        Task<Service> ArchiveServiceAsync(int serviceId); // Méthode pour archiver un service et retourner le service archivé
        Task<IEnumerable<Service>> SearchServicesAsync(string searchTerm); // Méthode pour rechercher des services
    }
}
