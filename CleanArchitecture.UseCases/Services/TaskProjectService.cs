using AutoMapper;
using CleanArchitecture.Entities.Projects;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.UseCases.Dtos.ProjectDtos.TaskDtos;
using CleanArchitecture.UseCases.InterfacesUse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Entities.Enum;
using CleanArchitecture.FramworkAndDrivers.Data.Repository;
namespace CleanArchitecture.UseCases.Services
{
    public class TaskProjectService : GenericService<TaskProject, TaskDto, TaskCreateDto, TaskUpdateDto>, ITaskProjectService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper; // Assuming you're using AutoMapper or similar for DTO mapping

        public TaskProjectService(ITaskRepository taskRepository, IMapper mapper) : base(taskRepository)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskDto>> GetTasksByProjectIdAsync(int projectId)
        {
            var tasks = await _taskRepository.GetTasksByProjectIdAsync(projectId);
            return _mapper.Map<IEnumerable<TaskDto>>(tasks);
        }

        public async Task<IEnumerable<TaskDto>> GetTasksByEmployeeIdAsync(int employeeId)
        {
            var tasks = await _taskRepository.GetTasksByEmployeeIdAsync(employeeId);
            return _mapper.Map<IEnumerable<TaskDto>>(tasks);
        }

        public async Task<TaskProgressDto> GetTaskProgressAsync(int taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            // Assuming you have logic to calculate task progress
            var progress = new TaskProgressDto
            {
                TaskId = task.Id,
                Progress = CalculateTaskProgress(task) // Implement this method as needed
            };
            return progress;
        }

        public async Task AssignTaskToEmployeeAsync(int taskId, int employeeId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task != null)
            {
                task.AssignedToId = employeeId.ToString();
                await _taskRepository.UpdateAsync(task);
            }
        }

        public async Task CompleteTaskAsync(int taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task != null)
            {
                task.Status = (TaskStatus)ProjectTaskStatus.Completed; // Use the new enum name
                await _taskRepository.UpdateAsync(task);
            }
        }

        protected override TaskDto MapToDto(TaskProject entity)
        {
            return _mapper.Map<TaskDto>(entity);
        }

        protected override TaskProject MapToEntity(TaskCreateDto createDto)
        {
            return _mapper.Map<TaskProject>(createDto);
        }

        protected override void MapToEntity(TaskUpdateDto updateDto, TaskProject entity)
        {
            _mapper.Map(updateDto, entity);
        }

        // Example of a method to calculate task progress
        private double CalculateTaskProgress(TaskProject task)
        {
            // Implement logic to calculate task progress based on your requirements
            return 0; // Placeholder
        }
    }
}
