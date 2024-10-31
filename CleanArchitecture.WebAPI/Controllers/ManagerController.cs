// Controllers/ManagerController.cs
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using CleanArchitecture.Entities.Projects;
using CleanArchitecture.Entities.Users;
using CleanArchitecture.UseCases.InterfacesUse;

[ApiController]
[Route("api/[controller]")]
public class ManagerController : ControllerBase
{
    private readonly IManagerService _managerService;

    public ManagerController(IManagerService managerService)
    {
        _managerService = managerService;
    }

    // Get a list of users in the manager's department
    [HttpGet("GetUsersInDepartment")]
    public async Task<IActionResult> GetUsersInDepartment([FromQuery] string managerEmail)
    {
        var users = await _managerService.GetUserInDepartmentAsync(managerEmail);
        return Ok(users);
    }

    // Assign a task to an employee
    [HttpPost("AssignTaskToEmployee")]
    public async Task<IActionResult> AssignTaskToEmployee([FromBody] AssignTaskDto assignTaskDto)
    {
        try
        {
            await _managerService.AssignTaskToEmployeeAsync(assignTaskDto.ManagerEmail, assignTaskDto.EmployeeEmail, assignTaskDto.Task);
            return Ok("Task assigned successfully.");
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // Get all tasks for the department
    [HttpGet("GetTasksForDepartment")]
    public async Task<IActionResult> GetTasksForDepartment([FromQuery] string managerEmail)
    {
        var tasks = await _managerService.GetTasksForDepartmentAsync(managerEmail);
        return Ok(tasks);
    }
}

// DTO for task assignment
public class AssignTaskDto
{
    public string? ManagerEmail { get; set; }
    public string? EmployeeEmail { get; set; }
    public TaskProject? Task { get; set; }
}
