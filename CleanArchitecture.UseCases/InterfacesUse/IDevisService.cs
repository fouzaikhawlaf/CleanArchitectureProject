using CleanArchitecture.Entities.Orders;
using CleanArchitecture.UseCases.Dtos.OrderDtos.Devis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.InterfacesUse
{
  public interface IDevisService :IGenericService<Devis, DevisDto, CreateDevisDto, UpdateDevisDto>
    {

      
            // Custom methods specific to Devis
        Task<IEnumerable<DevisDto>> GetArchivedDevisAsync();
        Task<IEnumerable<DevisDto>> GetBrouillonDevisAsync();
        Task<IEnumerable<DevisDto>> SearchDevisAsync(string searchTerm);

        // Methods to manage Devis status
        Task AcceptDevisAsync(int id); // Accept a Devis
        Task RejectDevisAsync(int id);  // Reject a Devis

        // Calculation methods
        double CalculateTotalAmount(Devis devis);
        double CalculateTotalTVA(Devis devis);

    }
}
