using CleanArchitecture.Entities.Orders;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.UseCases.Dtos.OrderDtos.BonDeReceptionDto;
using CleanArchitecture.UseCases.InterfacesUse;
using CleanArchitecture.UseCases.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Services
{
    public class BonDeReceptionService : GenericService<BonDeReception, BonDeReceptionDto, CreateBonDeReceptionDto, UpdateBonDeReceptionDto>, IBonDeReceptionService
    {
        private readonly IBonDeReceptionRepository _bonDeReceptionRepository;
        private readonly IOrderSupplierRepository _orderSupplierRepository;

        public BonDeReceptionService(IBonDeReceptionRepository bonDeReceptionRepository, IOrderSupplierRepository orderSupplierRepository)
            : base(bonDeReceptionRepository)
        {
            _bonDeReceptionRepository = bonDeReceptionRepository;
            _orderSupplierRepository = orderSupplierRepository;
        }

        public async Task ConfirmBonDeReceptionAsync(int id)
        {
            await _bonDeReceptionRepository.ConfirmAsync(id);
        }

        public async Task ArchiveBonDeReceptionAsync(int id)
        {
            await _bonDeReceptionRepository.ArchiveAsync(id);
        }

        public async Task MarkAsInspectedAsync(int id)
        {
            await _bonDeReceptionRepository.MarkAsInspectedAsync(id);
        }

        public async Task<IEnumerable<BonDeReceptionDto>> SearchBonDeReceptionsAsync(string searchTerm)
        {
            var bonDeReceptions = await _bonDeReceptionRepository.SearchAsync(searchTerm);
            return bonDeReceptions.Select(BonDeReceptionMapper.ToDto);
        }

        public async Task<IEnumerable<BonDeReceptionDto>> GetPendingReceptionsAsync()
        {
            var pendingReceptions = await _bonDeReceptionRepository.GetPendingReceptionsAsync();
            return pendingReceptions.Select(BonDeReceptionMapper.ToDto);
        }

        public async Task HandleReceptionDiscrepanciesAsync(int id, double discrepancyAmount)
        {
            var bonDeReception = await _bonDeReceptionRepository.GetByIdAsync(id);
            if (bonDeReception == null)
            {
                throw new KeyNotFoundException("Bon de Reception not found.");
            }

            bonDeReception.DiscrepancyAmount = discrepancyAmount;
            bonDeReception.IsFlaggedForReview = true;

            // Mettre à jour l'entité
            await _bonDeReceptionRepository.UpdateAsync(bonDeReception);
        }

        public async Task RevertBonDeReceptionAsync(int id)
        {
            await _bonDeReceptionRepository.RevertStatusAsync(id);
        }

        // Nouvelle méthode pour créer un bon de réception à partir d'une commande fournisseur
        public async Task CreateBonDeReceptionFromOrderSupplierAsync(int orderSupplierId)
        {
            var orderSupplier = await _orderSupplierRepository.GetByIdAsync(orderSupplierId);
            if (orderSupplier == null)
            {
                throw new KeyNotFoundException("Commande fournisseur non trouvée.");
            }

            var bonDeReception = new CreateBonDeReceptionDto
            {
                OrderSupplierId = orderSupplierId,
                ReceivedDate = DateTime.UtcNow,
                IsConfirmed = false,
                IsArchived = false,
                IsInspected = false,
                IsAccepted = false,
                DiscrepancyAmount = 0, // Ou une logique pour initialiser ce champ
                Items = orderSupplier.Items.Select(item => new CreateBonDeReceptionItemDto
                {
                    ItemId = item.Id,
                    ReceivedQuantity = item.Quantity // Ou la quantité reçue
                }).ToList()
            };

            // Créer le bon de réception
            await AddAsync(bonDeReception);
        }

        // Nouvelle méthode pour obtenir tous les bons de réception avec leurs commandes associées
        public async Task<IEnumerable<BonDeReceptionDto>> GetAllWithOrderSuppliersAsync()
        {
            var bonDeReceptions = await _bonDeReceptionRepository.GetAllWithOrderSuppliersAsync();
            return bonDeReceptions.Select(BonDeReceptionMapper.ToDto);
        }

        // Nouvelle méthode pour obtenir les bons de réception archivés
        public async Task<IEnumerable<BonDeReceptionDto>> GetArchivedBonDeReceptionsAsync()
        {
            var archivedReceptions = await _bonDeReceptionRepository.GetArchivedBonDeReceptionsAsync();
            return archivedReceptions.Select(BonDeReceptionMapper.ToDto);
        }

        protected override BonDeReception MapToEntity(CreateBonDeReceptionDto dto)
        {
            return BonDeReceptionMapper.ToEntity(dto);
        }

        protected override void MapToEntity(UpdateBonDeReceptionDto dto, BonDeReception entity)
        {
            BonDeReceptionMapper.UpdateEntity(dto, entity); // Mise à jour de l'entité
        }

        protected override BonDeReceptionDto MapToDto(BonDeReception entity)
        {
            return BonDeReceptionMapper.ToDto(entity);
        }
    }
}
