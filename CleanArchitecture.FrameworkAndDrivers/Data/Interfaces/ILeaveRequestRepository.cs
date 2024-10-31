using CleanArchitecture.Entities.Leaves;
using CleanArchitecture.FrameworksAndDrivers.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Interfaces
{
    public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
    {
        Task<IEnumerable<LeaveRequest>> GetByEmployeeIdAsync(string employeeId);
        Task<IEnumerable<LeaveRequest>> GetPendingLeaveRequestsAsync();
        // Ajoutez les méthodes manquantes ici
        Task<int> GetTotalLeaveRequestsAsync();
        Task<IEnumerable<LeaveRequest>> GetApprovedLeaveRequestsAsync();
        Task<IEnumerable<LeaveRequest>> GetRejectedLeaveRequestsAsync();
       
        Task<int> GetLeaveBalanceAsync(string employeeId);
        Task<double> GetAverageLeaveDaysAsync();
        Task<LeaveRequest> GetById(string leaveRequestId);
        Task Delete(string id);  // Supprimer une demande de congé
        Task Update(LeaveRequest leaveRequest);  // Mettre à jour une demande de congé
    }
}

