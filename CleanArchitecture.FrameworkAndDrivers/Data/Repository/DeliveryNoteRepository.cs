using CleanArchitecture.Entities.Orders.DeliveryNotes;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.FrameworksAndDrivers;
using CleanArchitecture.FrameworksAndDrivers.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Repository
{
    public class DeliveryNoteRepository : GenericRepository<DeliveryNote>,IDeliveryNoteRepository
    {
        private readonly AppDbContext _dbContext;

        public DeliveryNoteRepository(AppDbContext context) : base(context)
        {
            _dbContext = context;
        }

        // Get a DeliveryNote by OrderClientId
        public async Task<DeliveryNote> GetByOrderClientIdAsync(int orderClientId)
        {
            return await _dbContext.DeliveryNotes.FirstOrDefaultAsync(dn => dn.OrderClientId == orderClientId);
        }





        // Search DeliveryNotes by OrderClientId or DeliveryDate
        public async Task<IEnumerable<DeliveryNote>> SearchAsync(int? orderClientId, DateTime? deliveryDate)
        {
            var query = _dbContext.DeliveryNotes.AsQueryable();

            if (orderClientId.HasValue)
            {
                query = query.Where(dn => dn.OrderClientId == orderClientId.Value);
            }

            if (deliveryDate.HasValue)
            {
                query = query.Where(dn => dn.DeliveryDate.Date == deliveryDate.Value.Date);
            }

            return await query.ToListAsync();
        }

        // Get all archived (IsArchived = true) DeliveryNotes
        public async Task<IEnumerable<DeliveryNote>> GetArchivedAsync()
        {
            return await _dbContext.DeliveryNotes
                                   .Where(dn => dn.IsArchived)
                                   .ToListAsync();
        }

        // Mark a specific DeliveryNote as archived
        public async Task MarkAsArchivedAsync(int deliveryNoteId)
        {
            var deliveryNote = await _dbContext.DeliveryNotes.FirstOrDefaultAsync(dn => dn.Id == deliveryNoteId);
            if (deliveryNote != null)
            {
                deliveryNote.IsArchived = true; // Set the IsArchived flag to true
                _dbContext.DeliveryNotes.Update(deliveryNote);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}


