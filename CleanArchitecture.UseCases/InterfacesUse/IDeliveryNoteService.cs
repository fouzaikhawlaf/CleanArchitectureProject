using CleanArchitecture.Entities.Orders.DeliveryNotes;
using CleanArchitecture.UseCases.Dtos.OrderDtos.DeliveryNoteDto;
using CleanArchitecture.UseCases.Dtos.OrderDtos.Devis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.InterfacesUse
{
    public interface IDeliveryNoteService : IGenericService<DeliveryNote, DeliveryNoteDto, CreateDeliveryNoteDto, UpdateDeliveryNoteDto>
    {


        Task<DeliveryNoteDto> CreateFromOrderAsync(int orderClientId, DateTime deliveryDate);
        Task MarkOrderAsDeliveredAsync(int orderClientId);
      

        // New archive and search methods
        Task<IEnumerable<DeliveryNoteDto>> SearchDeliveryNotesAsync(int? orderClientId, DateTime? deliveryDate);
        Task<IEnumerable<DeliveryNoteDto>> GetArchivedDeliveryNotesAsync();
        Task MarkAsArchivedAsync(int deliveryNoteId);
        Task<byte[]> GeneratePdfAsync(int deliveryNoteId);
    }
}
