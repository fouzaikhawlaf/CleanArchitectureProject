using CleanArchitecture.Entities.Projects;
using CleanArchitecture.UseCases.Dtos.ProjectDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.InterfacesUse
{
    public interface IProjectService : IGenericService<Project, ProjectDto, ProjectCreateDto, ProjectUpdateDto>
    {
        Task<IEnumerable<ProjectDto>> GetProjectsByEmployeeIdAsync(int employeeId);
        Task<ProjectTimelineDto> UpdateProjectTimelineAsync(int projectId, ProjectTimelineUpdateDto updateDto);
        Task<ProjectBudgetDto> UpdateProjectBudgetAsync(int projectId, double actualCost);
        Task<ProjectRiskDto> AssessProjectRiskAsync(int projectId, RiskAssessmentDto assessment);


    }
}
