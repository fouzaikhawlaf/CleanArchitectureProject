using CleanArchitecture.Entities.Leaves;
using CleanArchitecture.Entities.Produit;
using CleanArchitecture.UseCases.Dtos.ItemDtos.ProduitDtos;
using CleanArchitecture.UseCases.Dtos.LeaveRequestDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.InterfacesUse
{
    public interface ILeaveRequestService : IGenericService<LeaveRequest, LeaveRequestDto, CreateLeaveRequestDto, UpdateLeaveRequestDto>
    {

      

        // Récupère les demandes de congé pour un employé spécifique
        Task<IEnumerable<LeaveRequestDto>> GetLeaveRequestsByEmployeeIdAsync(string employeeId);

        // Valide ou refuse une demande de congé (par un manager ou RH)
        Task<bool> ApproveLeaveRequestAsync(string id, bool isApproved, string managerComment);
        Task<LeaveRequestHistoryDto> GetLeaveRequestHistoryAsync(string leaveRequestId);
        Task NotifyEmployeeAsync(string leaveRequestId);
        Task<bool> CheckLeaveQuotaAsync(string employeeId, int numberOfDays);
        Task<LeaveStatisticsDto> GetLeaveStatisticsAsync();
        Task<LeaveRequestDto> GetById(string id);
        Task Update(string id, UpdateLeaveRequestDto updateDto);  // Mettre à jour une demande existante
        Task Delete(string id);  // Supprimer une demande de congé

    }
}
