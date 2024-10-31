using CleanArchitecture.Entities.Enum;
using CleanArchitecture.Entities.Orders;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.UseCases.Dtos.OrderDtos.Devis;
using CleanArchitecture.UseCases.InterfacesUse;
using CleanArchitecture.UseCases.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Services
{
    public class DevisService : GenericService<Devis, DevisDto, CreateDevisDto, UpdateDevisDto>, IDevisService
    {
        private readonly IDevisRepository _devisRepository;

        public DevisService(IDevisRepository devisRepository) : base(devisRepository)
        {
            _devisRepository = devisRepository;
        }

        // Using the DevisMapper to map Devis entity to DevisDto
        protected override DevisDto MapToDto(Devis devis)
        {
            return DevisMapper.ToDto(devis);
        }

        // Using the DevisMapper to map CreateDevisDto to Devis entity
        protected override Devis MapToEntity(CreateDevisDto createDevisDto)
        {
            return DevisMapper.ToEntity(createDevisDto);
        }

        protected override void MapToEntity(UpdateDevisDto updateDevisDto, Devis existingDevis)
        {
            // Simply modify the existing entity without returning anything
            DevisMapper.ToEntity(updateDevisDto, existingDevis); // Mapping with the DevisMapper
        }


        // Method to accept a Devis
        public async Task AcceptDevisAsync(int id)
        {
            var devis = await _devisRepository.GetByIdAsync(id);
            if (devis == null)
            {
                throw new KeyNotFoundException("Devis not found.");
            }

            if (devis.Status != DevisStatus.Pending)
            {
                throw new InvalidOperationException("Only pending devis can be accepted.");
            }

            devis.Status = DevisStatus.Accepted;
            await _devisRepository.UpdateAsync(devis);
        }

        // Method to reject a Devis
        public async Task RejectDevisAsync(int id)
        {
            var devis = await _devisRepository.GetByIdAsync(id);
            if (devis == null)
            {
                throw new KeyNotFoundException("Devis not found.");
            }

            if (devis.Status != DevisStatus.Pending)
            {
                throw new InvalidOperationException("Only pending devis can be rejected.");
            }

            devis.Status = DevisStatus.Rejected;
            await _devisRepository.UpdateAsync(devis);
        }

        // Specific methods for Devis
        public async Task<IEnumerable<DevisDto>> GetArchivedDevisAsync()
        {
            var archivedDevis = await _devisRepository.GetArchivedDevisAsync();
            return archivedDevis.Select(DevisMapper.ToDto);
        }

        public async Task<IEnumerable<DevisDto>> GetBrouillonDevisAsync()
        {
            var brouillonDevis = await _devisRepository.GetBrouillonDevisAsync();
            return brouillonDevis.Select(DevisMapper.ToDto);
        }

        public async Task<IEnumerable<DevisDto>> SearchDevisAsync(string searchTerm)
        {
            var foundDevis = await _devisRepository.SearchDevisAsync(searchTerm);
            return foundDevis.Select(DevisMapper.ToDto);
        }

        // Calculation methods
        public double CalculateTotalAmount(Devis devis)
        {
            return devis.Products.Sum(p => p.Price);
        }

        public double CalculateTotalTVA(Devis devis)
        {
            devis.TVA = CalculateTotalAmount(devis) * (double)devis.TVARate / 100;
            return devis.TotalAmount + devis.TVA;
        }
    }
}
