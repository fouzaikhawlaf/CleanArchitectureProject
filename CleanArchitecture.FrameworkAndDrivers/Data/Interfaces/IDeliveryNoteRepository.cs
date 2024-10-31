using CleanArchitecture.Entities.Orders.DeliveryNotes;
using CleanArchitecture.FrameworksAndDrivers.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Interfaces
{
    public interface IDeliveryNoteRepository : IGenericRepository<DeliveryNote>
    {

        Task<DeliveryNote> GetByOrderClientIdAsync(int orderClientId);
        // Search and Archive methods
        Task<IEnumerable<DeliveryNote>> SearchAsync(int? orderClientId, DateTime? deliveryDate);
        Task<IEnumerable<DeliveryNote>> GetArchivedAsync();
        Task MarkAsArchivedAsync(int deliveryNoteId);
    }
}
