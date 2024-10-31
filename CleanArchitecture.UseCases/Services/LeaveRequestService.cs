using CleanArchitecture.Entities.Enum;
using CleanArchitecture.Entities.Leaves;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.FrameworkAndDrivers.Data.Repository;
using CleanArchitecture.UseCases.Dtos.LeaveRequestDtos;
using CleanArchitecture.UseCases.InterfacesUse;
using CleanArchitecture.UseCases.Mappers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Services
{
    public class LeaveRequestService : GenericService<LeaveRequest, LeaveRequestDto, CreateLeaveRequestDto, UpdateLeaveRequestDto>, ILeaveRequestService
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILogger<LeaveRequestService> _logger;
        private readonly INotificationServiceEmail _notificationService; // Ajout du service de notification
        public LeaveRequestService(ILeaveRequestRepository leaveRequestRepository, ILogger<LeaveRequestService> logger, INotificationServiceEmail notificationService )
            : base(leaveRequestRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _logger = logger;
            _notificationService = notificationService; // Initialisation
        }

        // Récupère les demandes de congé pour un employé spécifique
        public async Task<IEnumerable<LeaveRequestDto>> GetLeaveRequestsByEmployeeIdAsync(string employeeId)
        {
            _logger.LogInformation($"Fetching leave requests for employee with ID {employeeId}");
            var leaveRequests = await _leaveRequestRepository.GetByEmployeeIdAsync(employeeId);
            var leaveRequestDtos = LeaveRequestMapper.MapToDtoList(leaveRequests);
            _logger.LogInformation($"Retrieved {leaveRequestDtos.Count()} leave requests for employee ID {employeeId}");
            return leaveRequestDtos;
        }

        // Valide ou refuse une demande de congé (par un manager ou RH)
        public async Task<bool> ApproveLeaveRequestAsync(string id, bool isApproved, string managerComment)
        {
            _logger.LogInformation($"Approving leave request ID {id} with approval status {isApproved}");

            // Récupère la demande de congé par ID
            var leaveRequest = await _leaveRequestRepository.GetById(id);
            if (leaveRequest == null)
            {
                _logger.LogWarning($"Leave Request with ID {id} not found.");
                throw new KeyNotFoundException($"Leave Request with ID {id} not found.");
            }

            // Modifie le statut de la demande de congé selon l'approbation
            leaveRequest.Status = isApproved ? LeaveStatus.Approved : LeaveStatus.Rejected;
            leaveRequest.ManagerComment = managerComment;
            leaveRequest.UpdatedDate = DateTime.UtcNow;

            // Met à jour la demande de congé dans la base de données
            await _leaveRequestRepository.UpdateAsync(leaveRequest);

            _logger.LogInformation($"Leave request ID {id} has been {(isApproved ? "approved" : "rejected")}.");
            return true;
        }

        // Récupère l'historique d'une demande de congé
        public async Task<LeaveRequestHistoryDto> GetLeaveRequestHistoryAsync(string leaveRequestId)
        {
            _logger.LogInformation($"Fetching history for leave request ID {leaveRequestId}");
            var leaveRequest = await _leaveRequestRepository.GetById(leaveRequestId);
            if (leaveRequest == null)
            {
                _logger.LogWarning($"Leave Request with ID {leaveRequestId} not found.");
                throw new KeyNotFoundException($"Leave Request with ID {leaveRequestId} not found.");
            }

            var historyDto = new LeaveRequestHistoryDto(leaveRequest.LeaveRequestId, leaveRequest.Employee?.FullName);
            _logger.LogInformation($"Successfully retrieved history for leave request ID {leaveRequestId}");
            return historyDto;
        }

        // Notifie l'employé concernant le statut de sa demande de congé
        public async Task NotifyEmployeeAsync(string leaveRequestId)
        {
            _logger.LogInformation($"Notifying employee about leave request ID {leaveRequestId}");

            // Récupérer la demande de congé par ID
            var leaveRequest = await _leaveRequestRepository.GetById(leaveRequestId);
            if (leaveRequest == null)
            {
                _logger.LogWarning($"Leave Request with ID {leaveRequestId} not found.");
                throw new KeyNotFoundException($"Leave Request with ID {leaveRequestId} not found.");
            }

            // Préparer le sujet et le corps de l'e-mail
            string subject = $"Statut de votre demande de congé ID {leaveRequestId}";
            string status = leaveRequest.Status == LeaveStatus.Approved ? "approuvée" : "refusée"; // Utilisation de LeaveStatus
            string body = $"Bonjour {leaveRequest.Employee.FullName},\n\n" +
                          $"Votre demande de congé ID {leaveRequestId} a été {status}.\n\n" +
                          $"Commentaire du manager : {leaveRequest.ManagerComment}\n\n" +
                          "Cordialement,\n" +
                          "L'équipe des ressources humaines.";

            try
            {
                // Envoie l'e-mail de notification
                await _notificationService.SendEmailAsync(leaveRequest.Employee.Email, subject, body);
                _logger.LogInformation($"Notification email sent to employee with ID {leaveRequest.EmployeeId}.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send notification email to employee ID {leaveRequest.EmployeeId}");
                throw; // Rejeter l'exception après avoir enregistré l'erreur
            }
        }


        // Vérifie si l'employé a suffisamment de jours de congé
        public async Task<bool> CheckLeaveQuotaAsync(string employeeId, int numberOfDays)
        {
            _logger.LogInformation($"Checking leave quota for employee ID {employeeId} with requested days {numberOfDays}");
            var leaveBalance = await _leaveRequestRepository.GetLeaveBalanceAsync(employeeId);
            var isQuotaSufficient = leaveBalance >= numberOfDays;

            _logger.LogInformation(isQuotaSufficient
                ? $"Employee ID {employeeId} has sufficient leave balance."
                : $"Employee ID {employeeId} does not have enough leave balance.");

            return isQuotaSufficient;
        }

        public async Task<LeaveStatisticsDto> GetLeaveStatisticsAsync()
        {
            var totalRequests = await _leaveRequestRepository.GetTotalLeaveRequestsAsync();
            var approvedRequests = (await _leaveRequestRepository.GetApprovedLeaveRequestsAsync()).Count(); // Compte les demandes approuvées
            var rejectedRequests = (await _leaveRequestRepository.GetRejectedLeaveRequestsAsync()).Count(); // Compte les demandes refusées
            var pendingRequests = totalRequests - (approvedRequests + rejectedRequests); // Calcul des demandes en attente
            var averageLeaveDays = await _leaveRequestRepository.GetAverageLeaveDaysAsync(); // Moyenne des jours de congé

            return new LeaveStatisticsDto(totalRequests, approvedRequests, rejectedRequests, pendingRequests, averageLeaveDays);
        }



        // Mapping CreateLeaveRequestDto to LeaveRequest
        protected  override LeaveRequest MapToEntity(CreateLeaveRequestDto createLeaveRequestDto)
        {
            return LeaveRequestMapper.MapToEntity(createLeaveRequestDto);
        }

        // Mapping UpdateLeaveRequestDto to LeaveRequest
        protected override void MapToEntity(UpdateLeaveRequestDto updateLeaveRequestDto, LeaveRequest leaveRequest)
        {
            LeaveRequestMapper.MapToEntity(updateLeaveRequestDto, leaveRequest);
        }

        // Mapping LeaveRequest to LeaveRequestDto
        protected override LeaveRequestDto MapToDto(LeaveRequest leaveRequest)
        {
            return LeaveRequestMapper.MapToDto(leaveRequest);
        }



        public  async Task<LeaveRequestDto> GetById(string id)
        {
            var leaveRequest = await _leaveRequestRepository.GetById(id);
            if (leaveRequest == null) return null;

            return LeaveRequestMapper.MapToDto(leaveRequest);
        }




        public async Task Update(string id, UpdateLeaveRequestDto updateDto)
        {
            var leaveRequest = await _leaveRequestRepository.GetById(id);
            if (leaveRequest == null)
            {
                throw new Exception("Leave request not found.");
            }

            LeaveRequestMapper.MapToEntity(updateDto, leaveRequest);
            await _leaveRequestRepository.UpdateAsync(leaveRequest);
        }




        public async Task Delete(string id)
        {
            await _leaveRequestRepository.Delete(id);
        }

    }
}
