using CleanArchitecture.Entities.Enum;
using CleanArchitecture.Entities.Leaves;
using CleanArchitecture.UseCases.Dtos.LeaveRequestDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Mappers
{
    public static class LeaveRequestMapper
    {
        // Mapper de CreateLeaveRequestDto vers LeaveRequest (création de la demande)
        public static LeaveRequest MapToEntity(CreateLeaveRequestDto createLeaveRequestDto)
        {
            return new LeaveRequest
            {
                StartDate = createLeaveRequestDto.StartDate,
                EndDate = createLeaveRequestDto.EndDate,
                Reason = createLeaveRequestDto.Reason,
                EmployeeId = createLeaveRequestDto.EmployeeId,
                Status = LeaveStatus.Pending, // Par défaut, statut "En attente"
                CreatedDate = DateTime.UtcNow
            };
        }

        // Mapper d'UpdateLeaveRequestDto vers LeaveRequest (mise à jour de la demande)
        public static void MapToEntity(UpdateLeaveRequestDto updateLeaveRequestDto, LeaveRequest leaveRequest)
        {
            leaveRequest.StartDate = updateLeaveRequestDto.StartDate;
            leaveRequest.EndDate = updateLeaveRequestDto.EndDate;
            leaveRequest.Reason = updateLeaveRequestDto.Reason;
            leaveRequest.Status = updateLeaveRequestDto.Status;
            leaveRequest.ManagerComment = updateLeaveRequestDto.ManagerComment;
            leaveRequest.UpdatedDate = DateTime.UtcNow;
        }

        // Mapper de LeaveRequest vers LeaveRequestDto (lecture des données)
        public static LeaveRequestDto MapToDto(LeaveRequest leaveRequest)
        {
            return new LeaveRequestDto
            {
                LeaveRequestId = leaveRequest.LeaveRequestId,
                StartDate = leaveRequest.StartDate,
                EndDate = leaveRequest.EndDate,
                Reason = leaveRequest.Reason,
                Status = leaveRequest.Status,
                EmployeeId = leaveRequest.EmployeeId,
                EmployeeName = leaveRequest.Employee?.FullName, // Si Employee est une relation
                ManagerComment = leaveRequest.ManagerComment,
                CreatedDate = leaveRequest.CreatedDate,
                UpdatedDate = leaveRequest.UpdatedDate
            };
        }




        // Mapper d'une liste de LeaveRequest vers une liste de LeaveRequestDto
        public static IEnumerable<LeaveRequestDto> MapToDtoList(IEnumerable<LeaveRequest> leaveRequests)
        {
            var leaveRequestDtos = new List<LeaveRequestDto>();

            foreach (var leaveRequest in leaveRequests)
            {
                leaveRequestDtos.Add(MapToDto(leaveRequest));
            }

            return leaveRequestDtos;
        }
    }

}
