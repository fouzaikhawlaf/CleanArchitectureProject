using CleanArchitecture.Entities.Projects;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.UseCases.Dtos.ProjectDtos;
using CleanArchitecture.UseCases.InterfacesUse;
using CleanArchitecture.UseCases.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CleanArchitecture.UseCases.Services
{
    public class ProjectService : GenericService<Project, ProjectDto, ProjectCreateDto, ProjectUpdateDto>, IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
            : base(projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<ProjectDto>> GetProjectsByEmployeeIdAsync(int employeeId)
        {
            var projects = await _projectRepository.GetProjectsByEmployeeIdAsync(employeeId);
            return projects.Select(p => p.MapToDto());
        }

        public async Task<ProjectTimelineDto> UpdateProjectTimelineAsync(int projectId, ProjectTimelineUpdateDto updateDto)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project == null)
                throw new KeyNotFoundException($"Project with id {projectId} not found."); // Updated exception

            project.StartDate = updateDto.NewStartDate;
            project.EndDate = updateDto.NewEndDate;

            await _projectRepository.UpdateAsync(project);

            return new ProjectTimelineDto
            {
                ProjectId = project.Id,
                StartDate = project.StartDate,
                EndDate = project.EndDate
            };
        }

        public async Task<ProjectBudgetDto> UpdateProjectBudgetAsync(int projectId, double actualCost)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project == null)
                throw new KeyNotFoundException($"Project with id {projectId} not found."); // Updated exception

            project.ActualCost = actualCost;

            await _projectRepository.UpdateAsync(project);

            return new ProjectBudgetDto
            {
                ProjectId = project.Id,
                Budget = project.Budget,
                ActualCost = project.ActualCost
            };
        }

        public async Task<ProjectRiskDto> AssessProjectRiskAsync(int projectId, RiskAssessmentDto assessment)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project == null)
                throw new KeyNotFoundException($"Project with id {projectId} not found."); // Updated exception

            project.RiskLevel = assessment.AssessedRiskLevel;

            await _projectRepository.UpdateAsync(project);

            return new ProjectRiskDto
            {
                ProjectId = project.Id,
                RiskLevel = project.RiskLevel
            };
        }

        
        protected override Project MapToEntity(ProjectCreateDto createDto)
        {
            return createDto.MapToEntity(); // Use your ProjectMapper method
        }

       protected override void MapToEntity(ProjectUpdateDto updateDto, Project entity)
        {
            updateDto.MapToEntity(entity); // Use your ProjectMapper method
        }

        protected override ProjectDto MapToDto(Project entity)
        {
            return entity.MapToDto(); // Use your ProjectMapper method
        }

    }
}
