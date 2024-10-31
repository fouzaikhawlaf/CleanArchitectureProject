using CleanArchitecture.Entities.Produit;
using CleanArchitecture.UseCases.Dtos.ItemDtos.ServiceDtos;

namespace CleanArchitecture.UseCases.Mappers
{
    public static class ServiceMapper
    {
        public static ServiceDto MapToDto(Service service)
        {
            return new ServiceDto
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                Price = service.Price,
                TaxRate = service.TaxRate,
                IsArchived = service.IsArchived,
                Total = service.Price + (service.Price * service.TaxRate / 100) // Calcul du total incluant la TVA
            };
        }

        public static Service MapToEntity(CreateServiceDto createServiceDto)
        {
            return new Service
            {
                Name = createServiceDto.Name,
                Description = createServiceDto.Description,
                Price = createServiceDto.Price,
                TaxRate = createServiceDto.TaxRate,
                IsArchived = false // Par défaut, un service nouvellement créé n'est pas archivé
            };
        }

        public static void MapToEntity(UpdateServiceDto updateServiceDto, Service service)
        {
            service.Name = updateServiceDto.Name;
            service.Description = updateServiceDto.Description;
            service.Price = updateServiceDto.Price ?? 0; // Si Price est null, mettre 0
            service.TaxRate = updateServiceDto.TaxRate ?? 0; // Si TaxRate est null, mettre 0
            service.IsArchived = updateServiceDto.IsArchived ?? false; // Si IsArchived est null, mettre false
        }

    }
}

