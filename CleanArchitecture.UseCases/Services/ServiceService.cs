using CleanArchitecture.Entities.Produit;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.Infrastructure.Data.Repository;
using CleanArchitecture.UseCases.Dtos.ItemDtos.ServiceDtos;
using CleanArchitecture.UseCases.InterfacesUse;
using CleanArchitecture.UseCases.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Services
{
    public class ServiceService: GenericService<Service, ServiceDto, CreateServiceDto, UpdateServiceDto>, IServiceService
    {
        private readonly IProduitSRepository _serviceRepository;

        public ServiceService(IProduitSRepository serviceRepository) : base(serviceRepository)
        {
            
            _serviceRepository = serviceRepository;
        }

    
    

        public async Task<ServiceDto> ArchiveServiceAsync(int serviceId)
        {
            var archivedService = await _serviceRepository.ArchiveServiceAsync(serviceId);
            return ServiceMapper.MapToDto(archivedService);
        }

        public async Task<IEnumerable<ServiceDto>> GetAllAvailableServicesAsync()
        {
            var services = await _serviceRepository.GetAllAvailableServicesAsync();
            return services.Select(ServiceMapper.MapToDto);
        }

        public async Task<ServiceDto> GetServiceByIdAsync(int serviceId)
        {
            var service = await _serviceRepository.GetServiceByIdAsync(serviceId);
            return ServiceMapper.MapToDto(service);
        }

        public async Task<IEnumerable<ServiceDto>> SearchServicesAsync(string searchTerm)
        {
            var services = await _serviceRepository.SearchServicesAsync(searchTerm);
            return services.Select(ServiceMapper.MapToDto);
        }


        protected override Service MapToEntity(CreateServiceDto createServiceDto)
        {
            return ServiceMapper.MapToEntity(createServiceDto);
        }

        protected override void MapToEntity(UpdateServiceDto updateServiceDto, Service service)
        {
            ServiceMapper.MapToEntity(updateServiceDto, service);
        }


        protected override ServiceDto MapToDto(Service service)
        {
            return ServiceMapper.MapToDto(service);
        }
    }

}

