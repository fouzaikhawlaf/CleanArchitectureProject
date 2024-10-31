using CleanArchitecture.Entities.Enum;
using CleanArchitecture.Entities.Projects;
using CleanArchitecture.Entities.Users;
using CleanArchitecture.UseCases.Dtos.ProjectDtos.EmployeeDtos;
using CleanArchitecture.UseCases.Dtos.ProjectDtos.TaskDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Dtos.ProjectDtos
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ProjectStatus Status { get; set; }
        public double Budget { get; set; }
        public double ActualCost { get; set; }
        public RiskLevel RiskLevel { get; set; }
        public List<TaskDto> Tasks { get; set; } = new List<TaskDto>();
        public List<EmployeeDto> Employees { get; set; } = new List<EmployeeDto>();
    }
}
