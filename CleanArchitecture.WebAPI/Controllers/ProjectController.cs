using CleanArchitecture.UseCases.Dtos.ProjectDtos;
using CleanArchitecture.UseCases.InterfacesUse;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        // CREATE: api/Project
        [HttpPost]
        public async Task<ActionResult<ProjectDto>> CreateProject(ProjectCreateDto projectCreateDto)
        {
            var project = await _projectService.AddAsync(projectCreateDto);
            return CreatedAtAction(nameof(GetProjectById), new { id = project.Id }, project);
        }

        // READ: api/Project
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAllProjects()
        {
            var projects = await _projectService.GetAllAsync();
            return Ok(projects);
        }

        // READ: api/Project/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetProjectById(int id)
        {
            var project = await _projectService.GetByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        // UPDATE: api/Project/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] ProjectUpdateDto projectUpdateDto)
        {
            if (id != projectUpdateDto.Id)
            {
                return BadRequest("Project ID mismatch.");
            }

            try
            {
                await _projectService.UpdateAsync(id, projectUpdateDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Project with Id = {id} not found");
            }
        }

        // DELETE: api/Project/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            await _projectService.DeleteAsync(id);
            return NoContent();
        }

        // Specific Function: Get Projects by Employee ID
        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjectsByEmployeeId(int employeeId)
        {
            var projects = await _projectService.GetProjectsByEmployeeIdAsync(employeeId);
            return Ok(projects);
        }

        // Specific Function: Update Project Timeline
        [HttpPut("{projectId}/timeline")]
        public async Task<ActionResult<ProjectTimelineDto>> UpdateProjectTimeline(int projectId, [FromBody] ProjectTimelineUpdateDto updateDto)
        {
            try
            {
                var timeline = await _projectService.UpdateProjectTimelineAsync(projectId, updateDto);
                return Ok(timeline);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Project with Id = {projectId} not found");
            }
        }

        // Specific Function: Update Project Budget
        [HttpPut("{projectId}/budget")]
        public async Task<ActionResult<ProjectBudgetDto>> UpdateProjectBudget(int projectId, [FromBody] double actualCost)
        {
            try
            {
                var budget = await _projectService.UpdateProjectBudgetAsync(projectId, actualCost);
                return Ok(budget);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Project with Id = {projectId} not found");
            }
        }

        // Specific Function: Assess Project Risk
        [HttpPut("{projectId}/risk")]
        public async Task<ActionResult<ProjectRiskDto>> AssessProjectRisk(int projectId, [FromBody] RiskAssessmentDto assessment)
        {
            try
            {
                var risk = await _projectService.AssessProjectRiskAsync(projectId, assessment);
                return Ok(risk);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Project with Id = {projectId} not found");
            }
        }
    }
}

