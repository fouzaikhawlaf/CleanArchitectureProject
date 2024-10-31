using CleanArchitecture.Entities.Enum;
using CleanArchitecture.Entities.Projects;
using CleanArchitecture.Entities.Users;
using CleanArchitecture.UseCases.Dtos.ProjectDtos;
using CleanArchitecture.UseCases.Dtos.ProjectDtos.EmployeeDtos;
using CleanArchitecture.UseCases.Dtos.ProjectDtos.TaskDtos;
using System.Linq;

namespace CleanArchitecture.UseCases.Mappers
{
    public static class ProjectMapper
    {
        public static ProjectDto MapToDto(this Project project)
        {
            return new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Status = project.Status,
                Budget = project.Budget,
                ActualCost = project.ActualCost,
                RiskLevel = project.RiskLevel,
                Tasks = project.Tasks.Select(t => t.MapToDto()).ToList(),
                Employees = project.TeamMembers.Select(e => e.MapToDto()).ToList()
            };
        }

        public static Project MapToEntity(this ProjectCreateDto projectCreateDto)
        {
            return new Project
            {
                Name = projectCreateDto.Name,
                Description = projectCreateDto.Description,
                StartDate = projectCreateDto.StartDate,
                EndDate = projectCreateDto.EndDate,
                Status = projectCreateDto.Status,
                Budget = projectCreateDto.Budget,
                ActualCost = 0, // Assuming new projects start with 0 actual cost
                RiskLevel = RiskLevel.Low, // Assuming a default risk level
                // Tasks and Employees would be added later as needed
            };
        }

        public static void MapToEntity(this ProjectUpdateDto projectUpdateDto, Project project)
        {
            project.Name = projectUpdateDto.Name;
            project.Description = projectUpdateDto.Description;
            project.StartDate = projectUpdateDto.StartDate;
            project.EndDate = projectUpdateDto.EndDate;
            project.Status = projectUpdateDto.Status;
            project.Budget = projectUpdateDto.Budget;
            // Tasks and Employees would be updated separately as needed
        }

        // Similar methods for mapping Task and Employee

        public static TaskDto MapToDto(this TaskProject task)
        {
            return new TaskDto
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                StartDate = task.StartDate,
                EndDate = task.EndDate,
                Status = task.Status,
                AssignedTo = task.AssignedTo as Employee

            };
        }

        // Méthode pour mapper les employés
        public static EmployeeDto MapToDto(this Employee employee)
        {
            return new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Department = employee.Department,
            
            };
        }

        // Nouvelle méthode ajoutée pour mapper EmployeeDto vers Employee
        public static Employee MapToEntity(this EmployeeDto employeeDto)
        {
            if (employeeDto == null) return null;

            return new Employee
            {
                Id = employeeDto.Id,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Email = employeeDto.Email,
                Department = employeeDto.Department,
               
            };
        }
    }
}
