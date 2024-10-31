using CleanArchitecture.Entities.Orders.DeliveryNotes;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.UseCases.Dtos.OrderDtos.DeliveryNoteDto;
using CleanArchitecture.UseCases.InterfacesUse;
using CleanArchitecture.UseCases.Mappers;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Services
{
    public class DeliveryNoteService :GenericService<DeliveryNote, DeliveryNoteDto, CreateDeliveryNoteDto, UpdateDeliveryNoteDto>, IDeliveryNoteService
    {
        private readonly IDeliveryNoteRepository _deliveryNoteRepository;
        private readonly IOrderClientRepository _orderClientRepository;
        public DeliveryNoteService(IDeliveryNoteRepository deliveryNoteRepository, IOrderClientRepository orderClientRepository): base(deliveryNoteRepository) // Pass repository to base class
        {
            _deliveryNoteRepository = deliveryNoteRepository;
            _orderClientRepository = orderClientRepository;
        }

        // Create a DeliveryNote from OrderClient
        public async Task<DeliveryNoteDto> CreateFromOrderAsync(int orderClientId, DateTime deliveryDate)
        {
            var orderClient = await _orderClientRepository.GetByIdAsync(orderClientId);
            if (orderClient == null)
                throw new KeyNotFoundException($"OrderClient with ID {orderClientId} not found.");

            // Créer le bon de livraison
            var deliveryNote = new DeliveryNote
            {
                OrderClientId = orderClient.Id,
                DeliveryDate = DateTime.Now,
                DeliveryNoteItems = orderClient.OrderItems.Select(item => new DeliveryItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                }).ToList()
            };

            await _deliveryNoteRepository.AddAsync(deliveryNote);
            return deliveryNote.MapToDto(); // Return the created DeliveryNote as a DTO
        }

        // Mark as Delivered (entire order delivered)
        public async Task MarkOrderAsDeliveredAsync(int orderClientId)
        {
            var deliveryNote = await _deliveryNoteRepository.GetByOrderClientIdAsync(orderClientId);
            if (deliveryNote == null)
                throw new KeyNotFoundException($"DeliveryNote for OrderClient ID {orderClientId} not found.");

            deliveryNote.IsDelivered = true;

            var orderClient = await _orderClientRepository.GetByIdAsync(orderClientId);
            if (orderClient != null)
            {
                orderClient.IsDelivered = true;
                await _orderClientRepository.UpdateAsync(orderClient);
            }

            await _deliveryNoteRepository.UpdateAsync(deliveryNote);
        }

       

        // Search Delivery Notes by OrderClientId or DeliveryDate
        public async Task<IEnumerable<DeliveryNoteDto>> SearchDeliveryNotesAsync(int? orderClientId, DateTime? deliveryDate)
        {
            var deliveryNotes = await _deliveryNoteRepository.SearchAsync(orderClientId, deliveryDate);
            return deliveryNotes.Select(dn => dn.MapToDto()).ToList();
        }

        // Get all archived DeliveryNotes (IsArchived = true)
        public async Task<IEnumerable<DeliveryNoteDto>> GetArchivedDeliveryNotesAsync()
        {
            var archivedNotes = await _deliveryNoteRepository.GetArchivedAsync();
            return archivedNotes.Select(dn => dn.MapToDto()).ToList();
        }

        // Mark a specific DeliveryNote as archived
        public async Task MarkAsArchivedAsync(int deliveryNoteId)
        {
            await _deliveryNoteRepository.MarkAsArchivedAsync(deliveryNoteId);
        }




        // Use DeliveryNoteMapper to map CreateDeliveryNoteDto to DeliveryNote entity
        protected override DeliveryNote MapToEntity(CreateDeliveryNoteDto createDto)
        {
            return createDto.MapToEntity();  // Reuse the mapper method
        }

        // Use DeliveryNoteMapper to map UpdateDeliveryNoteDto to an existing DeliveryNote entity
        protected override void MapToEntity(UpdateDeliveryNoteDto updateDto, DeliveryNote entity)
        {
            updateDto.MapToEntity(entity);  // Reuse the mapper method
        }

        // Use DeliveryNoteMapper to map DeliveryNote entity to DeliveryNoteDto
        protected override DeliveryNoteDto MapToDto(DeliveryNote entity)
        {
            return entity.MapToDto();  // Reuse the mapper method
        }



        // Générer un PDF pour un bon de livraison
        public async Task<byte[]> GeneratePdfAsync(int deliveryNoteId)
        {
            var deliveryNote = await _deliveryNoteRepository.GetByIdAsync(deliveryNoteId);
            if (deliveryNote == null)
            {
                throw new KeyNotFoundException($"Delivery note with ID {deliveryNoteId} not found.");
            }

            using (var memoryStream = new MemoryStream())
            {
                var document = new Document();
                PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                // Ajouter du contenu au document
                document.Add(new Paragraph($"Delivery Note ID: {deliveryNote.Id}"));
                document.Add(new Paragraph($"Order Client ID: {deliveryNote.OrderClientId}"));
                document.Add(new Paragraph($"Delivery Date: {deliveryNote.DeliveryDate}"));
                document.Add(new Paragraph($"Is Delivered: {deliveryNote.IsDelivered}"));

                // Ajouter des items de livraison
                foreach (var item in deliveryNote.DeliveryNoteItems)
                {
                    document.Add(new Paragraph($"Product ID: {item.ProductId}, Quantity: {item.Quantity}"));
                }

                document.Close();
                return memoryStream.ToArray();
            }
        }

    }

}

