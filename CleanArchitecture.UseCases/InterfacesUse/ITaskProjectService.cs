
using CleanArchitecture.UseCases.Dtos.ProjectDtos.TaskDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.InterfacesUse
{
    // ProjectManagement.Application/Interfaces/ITaskService.cs
    public interface ITaskProjectService : IGenericService<Task, TaskDto, TaskCreateDto, TaskUpdateDto>
    {
    
        Task<IEnumerable<TaskDto>> GetTasksByProjectIdAsync(int projectId);
        Task<IEnumerable<TaskDto>> GetTasksByEmployeeIdAsync(int employeeId);
        Task<TaskProgressDto> GetTaskProgressAsync(int projectId);
        Task AssignTaskToEmployeeAsync(int taskId, int employeeId);
        Task CompleteTaskAsync(int taskId);
    }
   
    
}
