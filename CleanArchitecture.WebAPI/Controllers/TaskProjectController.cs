using CleanArchitecture.UseCases.Dtos.ProjectDtos.TaskDtos;
using CleanArchitecture.UseCases.InterfacesUse;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebAPI.Controllers
{
     [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskProjectService _taskService;

        public TaskController(ITaskProjectService taskService)
        {
            _taskService = taskService;
        }

        // CREATE: api/Task
        [HttpPost]
        public async Task<ActionResult<TaskDto>> CreateTask([FromBody] TaskCreateDto taskCreateDto)
        {
            var task = await _taskService.AddAsync(taskCreateDto);
            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        // READ: api/Task
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetAllTasks()
        {
            var tasks = await _taskService.GetAllAsync();
            return Ok(tasks);
        }

        // READ: api/Task/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTaskById(int id)
        {
            var task = await _taskService.GetByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskUpdateDto taskUpdateDto)
        {
            // Validate the model state
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Ensure the ID in the route matches the ID in the DTO
            if (id != taskUpdateDto.Id)
            {
                return BadRequest("Task ID mismatch.");
            }

            try
            {
                // Attempt to update the task
                await _taskService.UpdateAsync(id, taskUpdateDto);
                return NoContent(); // Return 204 No Content on success
            }
            catch (KeyNotFoundException)
            {
                // Handle the case where the task is not found
                return NotFound($"Task with Id = {id} not found");
            }
            catch (Exception ex)
            {
                // Handle any unexpected exceptions
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        // DELETE: api/Task/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskService.DeleteAsync(id);
            return NoContent();
        }

        // Specific Function: Assign Task to Employee
        [HttpPost("{taskId}/assign/{employeeId}")]
        public async Task<IActionResult> AssignTaskToEmployee(int taskId, int employeeId)
        {
            await _taskService.AssignTaskToEmployeeAsync(taskId, employeeId);
            return NoContent();
        }

        // Specific Function: Complete Task
        [HttpPost("{taskId}/complete")]
        public async Task<IActionResult> CompleteTask(int taskId)
        {
            await _taskService.CompleteTaskAsync(taskId);
            return NoContent();
        }
    }
    
    
}
