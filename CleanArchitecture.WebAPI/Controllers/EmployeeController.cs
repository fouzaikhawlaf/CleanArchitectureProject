using CleanArchitecture.UseCases.Dtos.ProjectDtos.EmployeeDtos;
using CleanArchitecture.UseCases.InterfacesUse;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // CREATE: api/Employee
     
        // READ: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            var employees = await _employeeService.GetAllAsync();
            return Ok(employees);
        }

        // READ: api/Employee/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(string id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

      
       

        // GET: api/Employee/{id}/workload
        [HttpGet("{id}/workload")]
        public async Task<ActionResult<EmployeeWorkloadDto>> GetEmployeeWorkload(string id)
        {
            try
            {
                var workload = await _employeeService.GetEmployeeWorkloadAsync(id);
                return Ok(workload);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception )
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }

        // Additional Methods
        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesByProjectId(int projectId)
        {
            var employees = await _employeeService.GetEmployeesByProjectIdAsync(projectId);
            return Ok(employees);
        }

        [HttpPost("{employeeId}/assign/{projectId}")]
        public async Task<IActionResult> AssignEmployeeToProject(string employeeId, int projectId)
        {
            try
            {
                await _employeeService.AssignEmployeeToProjectAsync(employeeId, projectId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception )
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("{employeeId}/remove/{projectId}")]
        public async Task<IActionResult> RemoveEmployeeFromProject(string employeeId, int projectId)
        {
            try
            {
                await _employeeService.RemoveEmployeeFromProjectAsync(employeeId, projectId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception )
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }





        // Get the tasks assigned to the employee
        [HttpGet("GetAssignedTasks")]
        public async Task<IActionResult> GetAssignedTasks([FromQuery] string employeeEmail)
        {
            try
            {
                var tasks = await _employeeService.GetAssignedTasksAsync(employeeEmail);
                return Ok(tasks);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Update the status of an assigned task
        [HttpPost("UpdateTaskStatus")]
        public async Task<IActionResult> UpdateTaskStatus([FromBody] UpdateTaskStatusDto updateTaskStatusDto)
        {
            try
            {
                await _employeeService.UpdateTaskStatusAsync(
                    updateTaskStatusDto.EmployeeEmail,
                    updateTaskStatusDto.TaskId,
                    updateTaskStatusDto.Status
                );
                return Ok("Task status updated successfully.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    // DTO for updating task status
    public class UpdateTaskStatusDto
    {
        public string? EmployeeEmail { get; set; }
        public int TaskId { get; set; }
        public string? Status { get; set; }
    }
}

