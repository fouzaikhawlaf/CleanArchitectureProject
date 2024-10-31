using CleanArchitecture.Entities.Enum;
using CleanArchitecture.Entities.Leaves;
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
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {

        private readonly AppDbContext _context;

        public LeaveRequestRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        // Récupérer toutes les demandes de congé d'un employé spécifique
        public async Task<IEnumerable<LeaveRequest>> GetByEmployeeIdAsync(string employeeId)
        {
            return await _context.LeaveRequests
                .Where(lr => lr.EmployeeId == employeeId)
                .ToListAsync();
        }

        // Récupérer toutes les demandes de congé en attente
        public async Task<IEnumerable<LeaveRequest>> GetPendingLeaveRequestsAsync()
        {
            return await _context.LeaveRequests
                .Where(lr => lr.Status == LeaveStatus.Pending)
               
                .ToListAsync();
        }



        public async Task<int> GetTotalLeaveRequestsAsync()
        {
            // Retourne le nombre total de demandes de congé dans la base de données
            return await _context.LeaveRequests.CountAsync();
        }

        public async Task<IEnumerable<LeaveRequest>> GetApprovedLeaveRequestsAsync()
        {
            // Retourne toutes les demandes de congé dont le statut est "Approuvé"
            return await _context.LeaveRequests
                                 .Where(lr => lr.Status == LeaveStatus.Approved)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<LeaveRequest>> GetRejectedLeaveRequestsAsync()
        {
            // Retourne toutes les demandes de congé dont le statut est "Refusé"
            return await _context.LeaveRequests
                                 .Where(lr => lr.Status == LeaveStatus.Rejected)
                                 .ToListAsync();
        }
        public async Task<int> GetLeaveBalanceAsync(String employeeId)
        {
            // Supposons que chaque employé a un solde de congé dans une entité `Employee`
            var employee = await _context.Employees
                                         .Where(e => e.Id == employeeId)
                                         .FirstOrDefaultAsync();

            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {employeeId} not found.");
            }

            // Retourne le solde de congé de l'employé
            return employee.LeaveBalance;
        }

        public async Task<double> GetAverageLeaveDaysAsync()
        {
            // Calcule la moyenne du nombre de jours de congé demandés
            return await _context.LeaveRequests
                                 .Where(lr => lr.Status == LeaveStatus.Approved || lr.Status == LeaveStatus.Rejected)
                                 .AverageAsync(lr => (lr.EndDate - lr.StartDate).TotalDays);
        }


        // Implémentation de la méthode GetByIdAsync
        public async Task<LeaveRequest> GetById(string leaveRequestId)
        {
            if (string.IsNullOrEmpty(leaveRequestId))
            {
                throw new ArgumentException("LeaveRequestId cannot be null or empty", nameof(leaveRequestId));
            }

            // Recherche de la demande de congé par son ID dans la base de données
            var leaveRequest = await _context.LeaveRequests
                .Include(lr => lr.Employee) // Inclure les relations nécessaires, comme Employee
                .FirstOrDefaultAsync(lr => lr.LeaveRequestId == leaveRequestId); // Remplacez `LeaveRequestId` par le bon attribut

            if (leaveRequest == null)
            {
                // Lever une exception si l'objet n'est pas trouvé
                throw new KeyNotFoundException($"LeaveRequest with ID {leaveRequestId} not found.");
            }

            return leaveRequest; // Retourner la demande de congé trouvée
        }

        public async Task Delete(string id)
        {
            var leaveRequest = await GetById(id);
            if (leaveRequest != null)
            {
                _context.LeaveRequests.Remove(leaveRequest);
                await _context.SaveChangesAsync();
            }
        }



        public async Task Update(LeaveRequest leaveRequest)
        {
            _context.LeaveRequests.Update(leaveRequest);
            await _context.SaveChangesAsync();
        }
    }
}
