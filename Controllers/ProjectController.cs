using AutoMapper;
using BackendTascly.Data.ModelsDto.ProjectsDtos;
using BackendTascly.Data.ModelsDto.UsersDtos;
using BackendTascly.Entities;
using BackendTascly.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackendTascly.Controllers
{
    [Route("api/Projects")]
    [ApiController]
    [Authorize]
    public class ProjectController(IProjectService projectService, IMapper mapper) : ControllerBase
    {
        [HttpGet("Workspaces/{workspaceId}")]
        public async Task<ActionResult> GetProjectsByWorkspaceId(Guid workspaceId)
        {
            var projects = await projectService.GetProjectsByWorkspaceId(workspaceId);

            var projectsDto = mapper.Map<List<GetProject>>(projects);

            return Ok(projectsDto);
        }

        [HttpGet("{projectId:guid}")]
        public async Task<ActionResult> GetProjectById(Guid projectId)
        {
            var project = await projectService.GetProjectByIdAsync(projectId);
            if (project is null) return NotFound("Project not found.");

            var projectDto = mapper.Map<GetProject>(project);

            return Ok(projectDto);
        }

        [HttpPost("Workspaces/{workspaceId}")]
        public async Task<ActionResult> CreateProjectAsync(PostProject postProject, Guid workspaceId)
        {
            var projectEntity = mapper.Map<Project>(postProject);
            var userId = Guid.Parse(User.FindFirstValue("UserId")!);
            
            var result = await projectService.CreateProjectAsync(projectEntity, userId, workspaceId);
            if (!result) return BadRequest("Failed to create project.");
            return Ok("Project created successfully.");
        }

        [HttpDelete("{projectId:guid}")]
        public async Task<ActionResult> DeleteProjectAsync(Guid projectId)
        {
            var result = await projectService.DeleteProjectAsync(projectId);
            if (!result) return NotFound("Project not found or could not be deleted.");
            return Ok("Project deleted successfully.");
        }

        [HttpGet("{projectId}/Statuses")]
        public async Task<ActionResult> GetProjectStatuses(Guid projectId)
        {
            var statuses = await projectService.GetProjectStatuses(projectId);

            return Ok(statuses);
        }

        [HttpGet("{projectId}/Importances")]
        public async Task<ActionResult> GetProjectImportances(Guid projectId)
        {
            var importances = await projectService.GetProjectImportances(projectId);

            return Ok(importances);
        }
    }
}
