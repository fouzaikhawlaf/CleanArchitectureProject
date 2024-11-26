using CleanArchitecture.UseCases.Services;
using CleanArchitecture.UseCases.Dtos.LeaveRequestDtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using CleanArchitecture.UseCases.InterfacesUse;

namespace CleanArchitecture.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveRequestController : ControllerBase
    {
        private readonly ILeaveRequestService _leaveRequestService;

        public LeaveRequestController(ILeaveRequestService leaveRequestService)
        {
            _leaveRequestService = leaveRequestService;
        }

        // Créer une nouvelle demande de congé
        [HttpPost]
        public async Task<IActionResult> CreateLeaveRequest([FromBody] CreateLeaveRequestDto createLeaveRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _leaveRequestService.AddAsync(createLeaveRequestDto);
            return CreatedAtAction(nameof(GetLeaveRequestById), new { id = result.LeaveRequestId }, result);
        }

        // Obtenir une demande de congé par ID
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetLeaveRequestById(string id)
        {
            var leaveRequest = await _leaveRequestService.GetById(id);
            if (leaveRequest == null)
                return NotFound();

            return Ok(leaveRequest);
        }

        // Obtenir toutes les demandes de congé
        [HttpGet]
        public async Task<IActionResult> GetAllLeaveRequests()
        {
            var leaveRequests = await _leaveRequestService.GetAllAsync();
            return Ok(leaveRequests);
        }

        // Mettre à jour une demande de congé
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateLeaveRequest(string id, [FromBody] UpdateLeaveRequestDto updateLeaveRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var leaveRequest = await _leaveRequestService.GetById(id);
            if (leaveRequest == null)
                return NotFound();

            await _leaveRequestService.Update(id, updateLeaveRequestDto);
            return NoContent();
        }

        // Supprimer une demande de congé
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteLeaveRequest(string id)
        {
            var leaveRequest = await _leaveRequestService.GetById(id);
            if (leaveRequest == null)
                return NotFound();

            await _leaveRequestService.Delete(id);
            return NoContent();
        }

        // Approuver ou refuser une demande de congé
        [HttpPost("{id:int}/approve")]
        public async Task<IActionResult> ApproveLeaveRequest(string id, [FromBody] ApproveLeaveRequestDto approveLeaveRequestDto)
        {
            var result = await _leaveRequestService.ApproveLeaveRequestAsync(id, approveLeaveRequestDto.IsApproved, approveLeaveRequestDto.ManagerComment);
            if (!result)
                return BadRequest("Error in approving/rejecting the leave request.");

            return Ok();
        }

        // Notifier un employé concernant une demande de congé
        [HttpPost("{id:int}/notify")]
        public async Task<IActionResult> NotifyEmployee(string id)
        {
            await _leaveRequestService.NotifyEmployeeAsync(id);
            return Ok("Notification sent to employee.");
        }
    }
}
