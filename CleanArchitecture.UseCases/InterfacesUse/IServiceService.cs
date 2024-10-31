using CleanArchitecture.Entities.Clients;
using CleanArchitecture.Entities.Produit;
using CleanArchitecture.UseCases.Dtos.ClientDtos;
using CleanArchitecture.UseCases.Dtos.ItemDtos.ServiceDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.InterfacesUse
{
    public interface IServiceService : IGenericService<Service, ServiceDto, CreateServiceDto, UpdateServiceDto>
    {

        Task<IEnumerable<ServiceDto>> GetAllAvailableServicesAsync();
        Task<ServiceDto> GetServiceByIdAsync(int serviceId);
        Task<ServiceDto> ArchiveServiceAsync(int serviceId);
        Task<IEnumerable<ServiceDto>> SearchServicesAsync(string searchTerm);

    }
}
